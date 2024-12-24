namespace War
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Squad squad1 = new Squad();
            Squad squad2 = new Squad();

            squad1.Battle(squad2);
        }
    }

    class Soldier
    {
        protected string Name;

        public Soldier()
        {
            Name = "Рядовой";
            Health = 100;
            Damage = 10;
        }

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
            Console.WriteLine($"{Name} наносит {Damage} урона");

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
            Damage = 20;
        }
    }

    class Commando : Soldier
    {
        public Commando()
        {
            Name = "Десантник";
            Damage = 20;
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
        }

        public void Battle(Squad enemySquad)
        {
            while (_soldiers.Count > 0 && enemySquad._soldiers.Count > 0)
            {
                foreach (Soldier soldier in _soldiers)
                {
                    if (soldier.Health > 0)
                    {
                        int randomNumber = UserUtils.GenerateRandomNumber(0, enemySquad._soldiers.Count);
                        int damage = soldier.CalculateDamage();

                        enemySquad._soldiers[randomNumber].TakeDamage(damage);
                    }
                    else
                    {
                        _soldiers.Remove(soldier);
                    }

                    soldier.ShowInfo();
                }

                Console.ReadKey();

                foreach (Soldier soldier in enemySquad._soldiers)
                {
                    if (soldier.Health > 0)
                    {
                        int randomNumber = UserUtils.GenerateRandomNumber(0, _soldiers.Count);
                        int damage = soldier.CalculateDamage();

                        _soldiers[randomNumber].TakeDamage(damage);
                    }
                    else
                    {
                        enemySquad._soldiers.Remove(soldier);
                    }

                    soldier.ShowInfo();
                }
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
