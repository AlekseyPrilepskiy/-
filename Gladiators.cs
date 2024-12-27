using System.Data;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Linq;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int CommandToStart = 1;
            const int CommandToShowInfo = 2;
            const int CommandToExit = 3;

            bool isWork = true;

            Paladin paladin = new Paladin();
            Warrior warrior = new Warrior();
            Barbarian barbarian = new Barbarian();
            Spellcaster spellcaster = new Spellcaster();
            Rogue rogue = new Rogue();

            List<Character> characters = new List<Character>()
            {
                paladin, warrior, barbarian, spellcaster, rogue
            };

            Message message = new Message();
            
            message.Greet();

            Arena arena = new Arena(characters);

            while (isWork)
            {
                Console.WriteLine($"{CommandToStart} - Начать сражение");
                Console.WriteLine($"{CommandToShowInfo} - Информация о бойцах");
                Console.WriteLine($"{CommandToExit} - Выход\n");

                Console.Write("Введите команду: ");
                int.TryParse(Console.ReadLine(), out int userCommand);

                switch (userCommand)
                {
                    case CommandToStart:
                        arena.IsBattle();
                        break;

                    case CommandToShowInfo:
                        arena.ShowInfo();
                        break;

                    case CommandToExit:
                        isWork = arena.IsExit();
                        break;

                    default:
                        Console.Write("Неверная команда. Попробуйте снова.\n");
                        break;
                }
            }
        }
    }

    interface IDamageable
    {
        string Name { get; }
        int Health { get; }
        int Damage { get; }

        void TakeDamage(int damage);

        int CalculateDamage();
    }

    abstract class Character : IDamageable
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }

        public abstract Character Clone();

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health < 0)
            {
                Health = 0;
            }
        }

        public virtual int CalculateDamage()
        {
            return Damage;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Здоровье: {Health}. Урон: {Damage}");
        }
    }

    class Paladin : Character
    {
        private int _critRate = 20;
        private int _critDamage;
        private int _critСoefficient = 2;
        private int _lowRateBorder = 1;
        private int _hightRateBorder = 101;

        public Paladin()
        {
            Name = "Паладин";
            Health = 100;
            Damage = 17;
            _critDamage = Damage * _critСoefficient;
        }

        public override int CalculateDamage()
        {
            if (UserUtils.GenerateRandomNumber(_lowRateBorder, _hightRateBorder) > _critRate)
            {
                Console.WriteLine($"{Name} наносит удар и наносит {Damage} урона!");
                return Damage;
            }
            else
            {
                Console.WriteLine($"{Name} наносит критический удар и наносит {_critDamage} урона!");
                return _critDamage;
            }
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"{Name} может наносить удвоенный урон. Здоровье: {Health}. Урон: {Damage}");
        }

        public override Character Clone()
        {
            return new Paladin();
        }
    }

    class Warrior : Character
    {
        private int _doubleAttackDamage;
        private int _doubleAttackNumber = 3;
        private int _attackCounter = 0;

        public Warrior()
        {
            Name = "Воин";
            Health = 80;
            Damage = 20;
            _doubleAttackDamage = Damage + Damage;
        }

        public override int CalculateDamage()
        {
            _attackCounter++;

            if (_attackCounter == _doubleAttackNumber)
            {
                Console.WriteLine($"{Name} совершает две атаки и наносит {_doubleAttackDamage} урона!");
                _attackCounter = 0;

                return _doubleAttackDamage;
            }
            else
            {
                Console.WriteLine($"{Name} атакует и наносит {Damage} урона!");

                return Damage;
            }
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"{Name} каждую третью атаку бьет дважды. Здоровье: {Health}. Урон: {Damage}");
        }

        public override Character Clone()
        {
            return new Warrior();
        }
    }

    class Barbarian : Character
    {
        private bool _isCanHeal = true;
        private int _range = 0;
        private int _rangeForHeal = 80;
        private int _heal = 60;

        public Barbarian()
        {
            Name = "Варвар";
            Health = 120;
            Damage = 15;
        }

        public override int CalculateDamage()
        {
            Console.WriteLine($"{Name} наносит {Damage} урона!");

            return Damage;
        }

        public override void TakeDamage(int damage)
        {
            Health -= damage;
            _range += damage;

            if (_range >= _rangeForHeal && _isCanHeal == true)
            {
                _isCanHeal = false;

                Console.WriteLine($"{Name} впал в ярость и восстанавливает {_heal} здоровья.");
                Health += _heal;
            }

            if (Health < 0)
            {
                Health = 0;
            }
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"{Name} накапливает ярость и лечится. Здоровье: {Health}. Урон: {Damage}");
        }

        public override Character Clone()
        {
            return new Barbarian();
        }
    }

    class Spellcaster : Character
    {
        private int _fireballManacost = 33;
        private int _fireballDamage = 30;

        public Spellcaster()
        {
            Name = "Маг";
            Health = 50;
            Damage = 4;
            Mana = 100;
        }

        public int Mana { get; private set; }

        public override int CalculateDamage()
        {
            if (Mana >= _fireballManacost)
            {
                Mana -= _fireballManacost;

                Console.WriteLine($"{Name} наносит {_fireballDamage} урона огненным шаром!");
                Console.WriteLine($"Осталось маны: {Mana}");

                return _fireballDamage;
            }
            else
            {
                Console.WriteLine($"{Name} бьет бесполезной палкой и наносит {Damage} урона!");

                return Damage;
            }
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"{Name}, если есть мана, призывает огненный шар. Здоровье: {Health}. Урон: {Damage}. Мана: {Mana}");
        }

        public override Character Clone()
        {
            return new Spellcaster();
        }
    }

    class Rogue : Character
    {
        private int _chanceDodgeAttack = 30;
        private int _lowRateBorder = 1;
        private int _hightRateBorder = 101;

        public Rogue()
        {
            Name = "Плут";
            Health = 70;
            Damage = 12;
        }

        public override int CalculateDamage()
        {
            Console.WriteLine($"{Name} наносит {Damage} урона!");

            return Damage;
        }

        public override void TakeDamage(int damage)
        {
            if (UserUtils.GenerateRandomNumber(_lowRateBorder, _hightRateBorder) <= _chanceDodgeAttack)
            {
                Console.WriteLine($"{Name} увернулся от атаки! Урон не прошел.");
            }
            else
            {
                Health -= damage;
            }

            if (Health < 0)
            {
                Health = 0;
            }
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"{Name} может уклониться от атаки. Здоровье: {Health}. Урон: {Damage}\n");
        }

        public override Character Clone()
        {
            return new Rogue();
        }
    }

    class Message
    {
        public void Greet()
        {
            Console.WriteLine("Добро пожаловать на арену!");
            Console.WriteLine("Сегодня вам предстоит выбрать двух бойцов для сражения.\n");
        }

        public void ShowAllFighters(List<Character> characters)
        {
            Console.WriteLine("Список доступных бойцов: ");

            for (int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {characters[i].Name}");
            }

            Console.WriteLine();
        }
    }

    class Arena
    {
        private List<Character> _characters;
        private List<IDamageable> _fighters;
        private Message _message = new Message();
        private IDamageable _firstFighter;
        private IDamageable _secondFighter;

        public Arena(List<Character> characters)
        {
            _characters = characters;
            _message.ShowAllFighters(_characters);
        }

        public void IsBattle()
        {
            _fighters = ChooseFigthers();
            _firstFighter = _fighters[0];
            _secondFighter = _fighters[1];

            while (_firstFighter.Health > 0 && _secondFighter.Health > 0)
            {
                _secondFighter.TakeDamage(_firstFighter.CalculateDamage());

                Console.WriteLine($"У второго бойца осталось {_secondFighter.Health} хп");
                Console.ReadLine();

                if (_secondFighter.Health != 0)
                {
                    _firstFighter.TakeDamage(_secondFighter.CalculateDamage());

                    Console.WriteLine($"У первого бойца осталось {_firstFighter.Health} хп");
                    Console.ReadLine();
                }
            }

            DetermineWinner();
        }

        public void ShowInfo()
        {
            foreach (Character character in _characters)
            {
                character.ShowInfo();
            }
        }

        public bool IsExit()
        {
            Console.WriteLine("Программа завершила работу");

            return false;
        }

        private void DetermineWinner()
        {
            if (_firstFighter.Health == 0)
            {
                Console.WriteLine($"{_secondFighter.Name} - победил!");
            }
            else
            {
                Console.WriteLine($"{_firstFighter.Name} - победил!");
            }
        }

        private List<IDamageable> ChooseFigthers()
        {
            List<IDamageable> fighters = new List<IDamageable>();

            Console.Write("Выберите номер первого бойца: ");
            int userChoice1 = DetermineUserInput();

            Console.Write("Выберите номер второго бойца: ");
            int userChoice2 = DetermineUserInput();

            IDamageable fighter1 = _characters[userChoice1 - 1].Clone();
            IDamageable fighter2 = _characters[userChoice2 - 1].Clone();

            fighters.Add(fighter1);
            fighters.Add(fighter2);
            
            return fighters;
        }

        private int DetermineUserInput()
        {
            int firstElementIndex = 1;
            int lastElementIndex = _characters.Count;

            int.TryParse(Console.ReadLine(), out int userChoice);

            if (userChoice < firstElementIndex || userChoice > lastElementIndex)
            {
                Console.WriteLine("Неккоректный номер бойца. Будет выбран случайный боец.");
                userChoice = UserUtils.GenerateRandomNumber(firstElementIndex, lastElementIndex + 1);
            }

            return userChoice;
        }
    }

    class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }
    }
}
