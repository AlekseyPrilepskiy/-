namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            zoo.TakeTour();
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries = new List<Aviary>
        {
            new Aviary(), new Aviary(), new Aviary(), new Aviary()
        };

        public void TakeTour()
        {
            const int CommandToExit = 8;

            bool isOpen = true;

            Console.WriteLine("Добро пожаловать в наш зоопарк!");
            Console.WriteLine($"У нас есть {_aviaries.Count} вольеров с животными.\n");

            while (isOpen)
            {
                for (int i = 0; i < _aviaries.Count; i++)
                {
                    Console.WriteLine($"Вольер номер: {i + 1}");
                }

                Console.Write($"\nВыберите вольер к которому хотите подойти (Для выхода из зоопарка введите: {CommandToExit + 1}): ");

                int.TryParse(Console.ReadLine(), out int userInput);
                userInput--;

                if (userInput >= 0 && userInput < _aviaries.Count)
                {
                    _aviaries[userInput].ShowInfo();

                    Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться");
                    Console.ReadKey();
                }
                else if (userInput == CommandToExit)
                {
                    isOpen = false;
                }
                else
                {
                    Console.WriteLine("Неверный ввод пользователя. Попробуйте снова.");
                }
            }
        }
    }

    class Aviary
    {
        private Family _animalsFamily = new Family();

        public void ShowInfo()
        {
            _animalsFamily.ShowInfo();
        }
    }

    class Family
    {
        private List<Animal> _animals = new List<Animal>();

        public Family()
        {
            GenerateFamily();
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Всего {_animals.Count} животных.\n");

            for (int i = 0; i < _animals.Count; i++)
            {
                Console.Write($"{i + 1} - ");
                _animals[i].ShowInfo();
            }

            Console.WriteLine("\nПрямо перед вами находится один из представителей.");
            _animals.First().MakeSound();
        }

        private void GenerateFamily()
        {
            int minimalCount = 2;
            int maximalCount = 8;
            int animalsCount = UserUtils.GenerateRandomNumber(minimalCount, maximalCount);

            Animal animal = AnimalsBank.GenerateAnimal();

            for (int i = 0 ; i < animalsCount; i++)
            {
                _animals.Add(animal.Clone());
            }
        }
    }

    abstract class Animal
    {
        protected string Name;
        protected string Gender;

        public Animal()
        {
            Gender = GenerateGender();
        }

        public abstract void MakeSound();

        public abstract Animal Clone();

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Вид: {Name}. Пол - {Gender}");
        }

        private string GenerateGender()
        {
            string boyGender = "Самец";
            string girlGender = "Самка";
            List<string> genders = new List<string>() { boyGender, girlGender };

            return UserUtils.GenerateRandomNumber(0, genders.Count) == (genders.Count - 1) ? boyGender : girlGender;
        }
    }

    class Wolf : Animal
    {
        public Wolf()
        {
            Name = "Волк";
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} воет.");
        }

        public override Animal Clone()
        {
            return new Wolf();
        }
    }

    class Bear : Animal
    {
        public Bear()
        {
            Name = "Медведь";
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} устало смотрит и молчит.");
        }

        public override Animal Clone()
        {
            return new Bear();
        }
    }

    class Lion : Animal
    {
        public Lion()
        {
            Name = "Лев";
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} издает громкий рык.");
        }

        public override Animal Clone()
        {
            return new Lion();
        }
    }

    class Eagle : Animal
    {
        public Eagle()
        {
            Name = "Орёл";
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} издаёт клёкот.");
        }

        public override Animal Clone()
        {
            return new Eagle();
        }
    }

    class WildCat : Animal
    {
        public WildCat()
        {
            Name = "Рысь";
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name}, наевшись обедом, мурчит.");
        }

        public override Animal Clone()
        {
            return new WildCat();
        }
    }

    class Snake : Animal
    {
        public Snake()
        {
            Name = "Змея";
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} шипит.");
        }

        public override Animal Clone()
        {
            return new Snake();
        }
    }

    class AnimalsBank
    {
        private static List<Animal> s_animals = new List<Animal>
        {
            new Wolf(), new Bear(), new Lion(), new Eagle(), new WildCat(), new Snake()
        };

        public static Animal GenerateAnimal()
        {
            int randomIndex = UserUtils.GenerateRandomNumber(0, s_animals.Count);

            return s_animals[randomIndex];
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
