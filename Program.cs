using System.Data;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int PaladinNumber = 1;
            const int WarriorNumber = 2;
            const int BarbarianNumber = 3;
            const int SpellcasterNumber = 4;
            const int RogueNumber = 5;

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

            Console.WriteLine("Добро пожаловать на арену!");
            Console.WriteLine("Сегодня вам предстоит выбрать двух бойцов для сражения.");

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
                        Console.WriteLine($"{PaladinNumber} - Паладин");
                        Console.WriteLine($"{WarriorNumber} - Воин");
                        Console.WriteLine($"{BarbarianNumber} - Варвар");
                        Console.WriteLine($"{SpellcasterNumber} - Маг");
                        Console.WriteLine($"{RogueNumber} - Плут\n");

                        Console.Write("Выберите номер первого бойца: ");
                        int.TryParse(Console.ReadLine(), out int userChoice1);

                        IDamageable fighter1 = characters[userChoice1 - 1];

                        Console.Write("Выберите номер второго бойца: ");
                        int.TryParse(Console.ReadLine(), out int userChoice2);

                        IDamageable fighter2 = characters[userChoice2 - 1];

                        if (userChoice1 == userChoice2)
                        {
                            if (userChoice2 == PaladinNumber)
                            {
                                fighter2 = new Paladin();
                            }
                            else if (userChoice2 == WarriorNumber)
                            {
                                fighter2 = new Warrior();
                            }
                            else if (userChoice2 == BarbarianNumber)
                            {
                                fighter2 = new Barbarian();
                            }
                            else if (userChoice2 == SpellcasterNumber)
                            {
                                fighter2 = new Spellcaster();
                            }
                            else if (userChoice2 == RogueNumber)
                            {
                                fighter2 = new Rogue();
                            }
                        }

                        while (fighter1.Health > 0 && fighter2.Health > 0)
                        {
                            fighter2.TakeDamage(fighter1.DoDamage());
                            Console.WriteLine($"У второго бойца осталось {fighter2.Health} хп");

                            if (fighter2.Health <= 0)
                            {
                                Console.WriteLine("Боец 2 повержен!");
                                break;
                            }

                            Console.ReadLine();

                            fighter1.TakeDamage(fighter2.DoDamage());
                            Console.WriteLine($"У первого бойца осталось {fighter1.Health} хп");

                            if (fighter1.Health <= 0)
                            {
                                Console.WriteLine("Боец 1 повержен!");
                                break;
                            }

                            Console.ReadLine();
                        }

                        Console.WriteLine("Бой окончен. Арена закрывается.");
                        isWork = false;
                        break;

                    case CommandToShowInfo:
                        Console.WriteLine($"Паладин может наносить удвоенный урон. Здоровье: {paladin.Health}. Урон: {paladin.Damage}");
                        Console.WriteLine($"Воин каждую третью атаку бьет дважды. Здоровье: {warrior.Health}. Урон: {warrior.Damage}");
                        Console.WriteLine($"Варвар накапливает ярость и лечится. Здоровье: {barbarian.Health}. Урон: {barbarian.Damage}");
                        Console.WriteLine($"Маг, если есть мана, призывает огненный шар. Здоровье: {spellcaster.Health}. Урон: {spellcaster.Damage}. Мана: {spellcaster.Mana}");
                        Console.WriteLine($"Плут может уклониться от атаки. Здоровье: {rogue.Health}. Урон: {rogue.Damage}");
                        break;

                    case CommandToExit:
                        Console.WriteLine("Программа завершила работу");
                        isWork = false;
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
        int Health { get; }
        int Damage { get; }
        void TakeDamage(int damage);
        int DoDamage();
    }

    class UserUtils
    {
        private static Random s_random = new Random();
        private static int s_min = 1;
        private static int s_max = 101;

        public static int GenerateRandomNumber()
        {
            return s_random.Next(s_min, s_max);
        }
    }

    abstract class Character
    {
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
    }

    class Paladin : Character, IDamageable
    {
        private int _critRate = 20;
        private int _critDamage;
        public Paladin()
        {
            Health = 100;
            Damage = 10;
            _critDamage = Damage * 2;
        }

        public int DoDamage()
        {
            if (UserUtils.GenerateRandomNumber() > _critRate)
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

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }

    class Warrior : Character, IDamageable
    {
        private int _doubleAttackDamage;
        private int _doubleAttackNumber = 3;
        private int _attackCounter = 0;

        public Warrior()
        {
            Health = 80;
            Damage = 10;
            _doubleAttackDamage = Damage * 2;
        }

        public int DoDamage()
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

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }

    class Barbarian : Character, IDamageable
    {
        private bool _isCanHeal = true;
        private int _range = 0;
        private int _rangeForHeal = 80;
        private int _heal = 60;

        public Barbarian()
        {
            Health = 120;
            Damage = 10;
        }

        public int DoDamage()
        {
            Console.WriteLine($"Варвар наносит {Damage} урона!");

            return Damage;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            _range += damage;

            if (_range >= _rangeForHeal && _isCanHeal == true)
            {
                _isCanHeal = false;

                Console.WriteLine($"Варвар впал в ярость и восстанавливает {_heal} здоровья.");
                Health += _heal;
            }
        }
    }

    class Spellcaster : Character, IDamageable
    {
        private int _fireballManacost = 25;
        private int _fireballDamage = 20;

        public Spellcaster()
        {
            Health = 50;
            Damage = 4;
            Mana = 100;
        }

        public int Mana { get; private set; }

        public int DoDamage()
        {
            if (Mana >= _fireballManacost)
            {
                Console.WriteLine($"Маг наносит {_fireballDamage} урона огненным шаром!");
                Mana -= _fireballManacost;

                return _fireballDamage;
            }
            else
            {
                Console.WriteLine($"Маг бьет бесполезной палкой и наносит {Damage} урона!");

                return Damage;
            }
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }

    class Rogue : Character, IDamageable
    {
        private int _chanceDodgeAttack = 30;

        public Rogue()
        {
            Health = 70;
            Damage = 10;
        }

        public int DoDamage()
        {
            Console.WriteLine($"Плут наносит {Damage} урона!");
            return Damage;
        }

        public void TakeDamage(int damage)
        {
            if (UserUtils.GenerateRandomNumber() <= _chanceDodgeAttack)
            {
                Console.WriteLine("Плут увернулся от атаки! Урон не прошел.");
            }
            else
            {
                Health -= damage;
            }
        }
    }
}