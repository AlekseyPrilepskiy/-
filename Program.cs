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

            List<IDamageable> characters = new List<IDamageable>()
            {
                paladin, warrior, barbarian, spellcaster, rogue
            };

            Greeting();

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
                        isWork = Start(characters);
                        break;

                    case CommandToShowInfo:
                        ShowInfo(characters);
                        break;

                    case CommandToExit:
                        isWork = Exit();
                        break;

                    default:
                        Console.Write("Неверная команда. Попробуйте снова.\n");
                        break;
                }
            }
        }

        private static void Greeting()
        {
            Console.WriteLine("Добро пожаловать на арену!");
            Console.WriteLine("Сегодня вам предстоит выбрать двух бойцов для сражения.");
        }

        private static void ShowAllFighters()
        {
            const int PaladinNumber = 1;
            const int WarriorNumber = 2;
            const int BarbarianNumber = 3;
            const int SpellcasterNumber = 4;
            const int RogueNumber = 5;

            Console.WriteLine($"{PaladinNumber} - Паладин");
            Console.WriteLine($"{WarriorNumber} - Воин");
            Console.WriteLine($"{BarbarianNumber} - Варвар");
            Console.WriteLine($"{SpellcasterNumber} - Маг");
            Console.WriteLine($"{RogueNumber} - Плут\n");
        }

        private static bool Start(List<IDamageable> characters)
        {
            bool isFight = true;

            ShowAllFighters();

            Console.Write("Выберите номер первого бойца: ");
            int userChoice1 = DetermineUserInput(characters);

            Console.Write("Выберите номер второго бойца: ");
            int userChoice2 = DetermineUserInput(characters);

            IDamageable fighter1 = characters[userChoice1 - 1];
            IDamageable fighter2 = characters[userChoice2 - 1];

            if (userChoice1 == userChoice2)
            {
                fighter2 = fighter1.Clone();
            }

            while (isFight)
            {
                fighter2.TakeDamage(fighter1.DoDamage());
                Console.WriteLine($"У второго бойца осталось {fighter2.Health} хп");

                if (fighter2.Health == 0)
                {
                    Console.WriteLine("Боец 2 повержен!");
                    isFight = false;
                }

                Console.ReadLine();

                if (fighter2.Health != 0)
                {
                    fighter1.TakeDamage(fighter2.DoDamage());
                    Console.WriteLine($"У первого бойца осталось {fighter1.Health} хп");

                    if (fighter1.Health == 0)
                    {
                        Console.WriteLine("Боец 1 повержен!");
                        isFight = false;
                    }

                    Console.ReadLine();
                }
            }

            Console.WriteLine("Бой окончен. Арена закрывается.");
            return false;
        }

        private static void ShowInfo(List<IDamageable> characters)
        {
            foreach (Character character in characters)
            {
                character.ShowInfo();
            }
        }

        private static bool Exit()
        {
            Console.WriteLine("Программа завершила работу");
            return false;
        }

        private static int DetermineUserInput(List<IDamageable> characters)
        {
            int firstElementIndex = 1;
            int lastElementIndex = characters.Count;

            int.TryParse(Console.ReadLine(), out int userChoice);

            if (userChoice < firstElementIndex || userChoice > lastElementIndex)
            {
                Console.WriteLine("Неккоректный номер бойца. Будет выбран случайный боец.");
                userChoice = UserUtils.GenerateRandomNumber(firstElementIndex, lastElementIndex + 1);
            }

            return userChoice;
        }

        interface IDamageable
        {
            int Health { get; }
            int Damage { get; }

            void TakeDamage(int damage);

            int DoDamage();

            Character Clone();
        }

        class UserUtils
        {
            private static Random s_random = new Random();

            public static int GenerateRandomNumber(int min, int max)
            {
                return s_random.Next(min, max);
            }
        }

        abstract class Character : IDamageable
        {
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

            public virtual int DoDamage()
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
            private int _lowRateBorder = 1;
            private int _hightRateBorder = 101;

            public Paladin()
            {
                Health = 100;
                Damage = 17;
                _critDamage = Damage * 2;
            }

            public override int DoDamage()
            {
                if (UserUtils.GenerateRandomNumber(_lowRateBorder, _hightRateBorder) > _critRate)
                {
                    Console.WriteLine($"Паладин наносит удар и наносит {Damage} урона!");
                    return Damage;
                }
                else
                {
                    Console.WriteLine($"Паладин наносит критический удар и наносит {_critDamage} урона!");
                    return _critDamage;
                }
            }

            public override void ShowInfo()
            {
                Console.WriteLine($"Паладин может наносить удвоенный урон. Здоровье: {Health}. Урон: {Damage}");
            }

            public override Paladin Clone()
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
                Health = 80;
                Damage = 20;
                _doubleAttackDamage = Damage * 2;
            }

            public override int DoDamage()
            {
                _attackCounter++;

                if (_attackCounter == _doubleAttackNumber)
                {
                    Console.WriteLine($"Воин совершает две атаки и наносит {_doubleAttackDamage} урона!");
                    _attackCounter = 0;

                    return Damage * 2;
                }
                else
                {
                    Console.WriteLine($"Воин атакует и наносит {Damage} урона!");

                    return Damage;
                }
            }

            public override void ShowInfo()
            {
                Console.WriteLine($"Воин каждую третью атаку бьет дважды. Здоровье: {Health}. Урон: {Damage}");
            }

            public override Warrior Clone()
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
                Health = 120;
                Damage = 15;
            }

            public override int DoDamage()
            {
                Console.WriteLine($"Варвар наносит {Damage} урона!");

                return Damage;
            }

            public override void TakeDamage(int damage)
            {
                Health -= damage;
                _range += damage;

                if (_range >= _rangeForHeal && _isCanHeal == true)
                {
                    _isCanHeal = false;

                    Console.WriteLine($"Варвар впал в ярость и восстанавливает {_heal} здоровья.");
                    Health += _heal;
                }

                if (Health < 0)
                {
                    Health = 0;
                }
            }

            public override void ShowInfo()
            {
                Console.WriteLine($"Варвар накапливает ярость и лечится. Здоровье: {Health}. Урон: {Damage}");
            }

            public override Barbarian Clone()
            {
                return new Barbarian();
            }
        }

        class Spellcaster : Character
        {
            private int _fireballManacost = 33;
            private int _fireballDamage = 30;

            public int Mana { get; private set; }

            public Spellcaster()
            {
                Health = 50;
                Damage = 4;
                Mana = 100;
            }

            public override int DoDamage()
            {
                if (Mana >= _fireballManacost)
                {
                    Mana -= _fireballManacost;

                    Console.WriteLine($"Маг наносит {_fireballDamage} урона огненным шаром!");
                    Console.WriteLine($"Осталось маны: {Mana}");

                    return _fireballDamage;
                }
                else
                {
                    Console.WriteLine($"Маг бьет бесполезной палкой и наносит {Damage} урона!");

                    return Damage;
                }
            }

            public override void ShowInfo()
            {
                Console.WriteLine($"Маг, если есть мана, призывает огненный шар. Здоровье: {Health}. Урон: {Damage}. Мана: {Mana}");
            }

            public override Spellcaster Clone()
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
                Health = 70;
                Damage = 12;
            }

            public override int DoDamage()
            {
                Console.WriteLine($"Плут наносит {Damage} урона!");

                return Damage;
            }

            public override void TakeDamage(int damage)
            {
                if (UserUtils.GenerateRandomNumber(_lowRateBorder, _hightRateBorder) <= _chanceDodgeAttack)
                {
                    Console.WriteLine("Плут увернулся от атаки! Урон не прошел.");
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
                Console.WriteLine($"Плут может уклониться от атаки. Здоровье: {Health}. Урон: {Damage}");
            }

            public override Rogue Clone()
            {
                return new Rogue();
            }
        }
    }
}
