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
        public Soldier()
        {
            Name = "Рядовой";
            Health = 100;
            Damage = 10;
        }

        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }

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

        public virtual int CalculateDamage()
        {
            return Damage;
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

        public override int CalculateDamage()
        {
            return _critDamage;
        }
    }

    class Marine : Soldier
    {
        public Marine()
        {
            Name = "Морпех";
            Damage = 15;
        }
    }

    class Commando : Soldier
    {
        public Commando()
        {
            Name = "Десантник";
            Damage = 15;
        }
    }

    class Squad
    {
        private Soldier _soldier;
        private Sergeant _sergeant;
        private Marine _marine;
        private Commando _commando;

        public Squad()
        {
            _soldier = new Soldier();
            _sergeant = new Sergeant();
            _marine = new Marine();
            _commando = new Commando();

            _soldiers = new List<Soldier>()
            {
                _soldier, _sergeant, _marine, _commando
            };
        }

        public List<Soldier> _soldiers { get; private set; }

        public void Fight(Squad enemySquad)
        {
            foreach (Soldier soldier in _soldiers)
            {
                if (enemySquad._soldiers.Count > 0)
                {
                    int randomFighter = UserUtils.GenerateRandomNumber(0, enemySquad._soldiers.Count);
                    Soldier attackedSoldier = enemySquad._soldiers[randomFighter];

                    if (soldier.Name == _marine.Name && enemySquad._soldiers.Count > 1)
                    {
                        int randomFighter2 = randomFighter;

                        attackedSoldier.TakeDamage(soldier.CalculateDamage());
                        TrackHealth(enemySquad, attackedSoldier);

                        if (enemySquad._soldiers.Count > 0)
                        {
                            randomFighter2 = UserUtils.GenerateRandomNumber(0, enemySquad._soldiers.Count);
                            attackedSoldier = enemySquad._soldiers[randomFighter2];

                            while (randomFighter2 == randomFighter && enemySquad._soldiers.Count > 1)
                            {
                                randomFighter = UserUtils.GenerateRandomNumber(0, enemySquad._soldiers.Count);
                                attackedSoldier = enemySquad._soldiers[randomFighter];
                            }

                            attackedSoldier.TakeDamage(soldier.CalculateDamage());
                            TrackHealth(enemySquad, attackedSoldier);
                        }
                    }
                    else if (soldier.Name == _commando.Name)
                    {
                        attackedSoldier.TakeDamage(soldier.CalculateDamage());
                        TrackHealth(enemySquad, attackedSoldier);

                        if (enemySquad._soldiers.Count > 0)
                        {
                            int randomFighter2 = UserUtils.GenerateRandomNumber(0, enemySquad._soldiers.Count);
                            attackedSoldier = enemySquad._soldiers[randomFighter2];

                            attackedSoldier.TakeDamage(soldier.CalculateDamage());
                            TrackHealth(enemySquad, attackedSoldier);
                        }
                    }
                    else
                    {
                        attackedSoldier.TakeDamage(soldier.CalculateDamage());
                        TrackHealth(enemySquad, attackedSoldier);
                    }
                }


            }

            Console.WriteLine();
        }

        public void TrackHealth(Squad squad, Soldier soldier)
        {
            if (soldier.Health == 0)
            {
                squad._soldiers.Remove(soldier);
            }
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
            while (_squad1._soldiers.Count > 0 && _squad2._soldiers.Count > 0)
            {
                Console.WriteLine("Ход первого отряда.");

                _squad1.ShowInfo();
                Console.WriteLine();
                _squad1.Fight(_squad2);

                if (_squad2._soldiers.Count != 0)
                {
                    Console.WriteLine("Ход второго отряда.");

                    _squad2.ShowInfo();
                    Console.WriteLine();
                    _squad2.Fight(_squad1);
                }

                Console.Clear();
            }
        }

        public void ShowWinner()
        {
            if (_squad1._soldiers.Count > 0)
            {
                Console.WriteLine("Победил отряд 1");
            }
            else if (_squad2._soldiers.Count > 0)
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
