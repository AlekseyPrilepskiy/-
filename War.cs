using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Battleground battleground = new Battleground();
            battleground.Battle();
            battleground.ShowWinner();
        }
    }

    class Soldier
    {
        protected string Name;
        protected int Damage;

        public Soldier()
        {
            Name = "Рядовой";
            Health = 100;
            Damage = 10;
        }

        public int Health { get; protected set; }

        public virtual void TakeDamage(int damage)
        {
            Console.WriteLine($"{Name} получает {damage} урона.");
            Health -= damage;

            if (Health <= 0)
            {
                Console.WriteLine($"{Name} погибает.");
                Health = 0;
            }
        }

        public virtual void Attack(List<Soldier> enemySoldiers)
        {
            if (enemySoldiers.Count > 0)
            {
                int randomFighter = UserUtils.GenerateRandomNumber(0, enemySoldiers.Count);

                enemySoldiers[randomFighter].TakeDamage(Damage);
            }
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"{Name} осталось здоровья: {Health}.");
        }
    }

    class Sergeant : Soldier
    {
        private int _critDamage;
        private int _critCoefficient;

        public Sergeant()
        {
            Name = "Сержант";
            _critCoefficient = 2;
            _critDamage = Damage * _critCoefficient;
        }

        public override void Attack(List<Soldier> enemySoldiers)
        {
            if (enemySoldiers.Count > 0)
            {
                int randomFighter = UserUtils.GenerateRandomNumber(0, enemySoldiers.Count);

                enemySoldiers[randomFighter].TakeDamage(Damage);
            }
        }
    }

    class Marine : Soldier
    {
        public Marine()
        {
            Name = "Морпех";
            Damage = 15;
        }

        public override void Attack(List<Soldier> enemySoldiers)
        {
            if (enemySoldiers.Count > 0)
            {
                int randomFighter = UserUtils.GenerateRandomNumber(0, enemySoldiers.Count);
                int randomFighter2 = randomFighter;

                enemySoldiers[randomFighter].TakeDamage(Damage);

                while (randomFighter == randomFighter2 && enemySoldiers.Count > 1)
                {
                    randomFighter2 = UserUtils.GenerateRandomNumber(0, enemySoldiers.Count);
                }

                if (enemySoldiers.Count > 1)
                {
                    enemySoldiers[randomFighter2].TakeDamage(Damage);
                }

            }
        }
    }

    class Commando : Soldier
    {
        public Commando()
        {
            Name = "Десантник";
            Damage = 15;
        }

        public override void Attack(List<Soldier> enemySoldiers)
        {
            if (enemySoldiers.Count > 0)
            {
                int randomFighter = UserUtils.GenerateRandomNumber(0, enemySoldiers.Count);

                enemySoldiers[randomFighter].TakeDamage(Damage);

                randomFighter = UserUtils.GenerateRandomNumber(0, enemySoldiers.Count);

                if (enemySoldiers[randomFighter].Health > 0)
                {
                    randomFighter = UserUtils.GenerateRandomNumber(0, enemySoldiers.Count);

                    enemySoldiers[randomFighter].TakeDamage(Damage);
                }
                else
                {
                    Console.WriteLine($"{Name} попал по убитому, поэтому пропускает свой ход.");
                }
            }
        }
    }

    class Squad
    {
        private List<Soldier> _soldiers;

        public Squad()
        {
            _soldiers = new List<Soldier>()
            {
                new Soldier(), new Sergeant(), new Marine(), new Commando()
            };

            SoldiersCount = _soldiers.Count;
        }

        public int SoldiersCount { get; private set; }

        public void Fight(Squad enemySquad)
        {
            foreach (Soldier soldier in _soldiers)
            {
                soldier.Attack(enemySquad._soldiers);
                enemySquad.TrackHealth();
                Console.ReadKey();
            }
        }

        public void TrackHealth()
        {
            _soldiers.RemoveAll(soldier => soldier.Health == 0);
            SoldiersCount = _soldiers.Count;
        }

        public void ShowInfo()
        {
            Console.WriteLine("Здоровье текущего отряда: ");

            foreach (Soldier soldier in _soldiers)
            {
                soldier.ShowInfo();
            }
        }
    }

    class Battleground
    {
        private Squad _squad1 = new Squad();
        private Squad _squad2 = new Squad();

        public void Battle()
        {
            while (_squad1.SoldiersCount > 0 && _squad2.SoldiersCount > 0)
            {
                Console.WriteLine("Ход первого отряда.");

                _squad1.ShowInfo();
                Console.WriteLine();
                _squad1.Fight(_squad2);

                if (_squad2.SoldiersCount != 0)
                {
                    Console.WriteLine("\nХод второго отряда.");

                    _squad2.ShowInfo();
                    Console.WriteLine();
                    _squad2.Fight(_squad1);
                }

                Console.Clear();
            }
        }

        public void ShowWinner()
        {
            if (_squad1.SoldiersCount > 0)
            {
                Console.WriteLine("Победил отряд 1");
            }
            else if (_squad2.SoldiersCount > 0)
            {
                Console.WriteLine("Победил отряд 2");
            }
        }
    }

    class UserUtils
    {
        private static Random s_randomNumber = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return s_randomNumber.Next(min, max);
        }
    }
}
