namespace ConsoleApp32
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>
            {
                new Wolf(), new Bear(), new Lion(), new Eagle(), new WildCat(), new Snake()
            };

            Zoo zoo = new Zoo(animals);
            zoo.StartTour();
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries = new List<Aviary>();

        public Zoo(List<Animal> animals)
        {
            FillAviaries(animals);
        }

        public void StartTour()
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

        private void FillAviaries(List<Animal> animals)
        {
            int minimalAviariesCount = 4;
            int maximalAviariesCount = 6;
            int aviariesCount = UserUtils.GenerateRandomNumber(minimalAviariesCount, maximalAviariesCount);

            for (int i = 0; i < aviariesCount; i++)
            {
                Animal animal = animals[UserUtils.GenerateRandomNumber(0, animals.Count - 1)];

                _aviaries.Add(new Aviary(animal));
            }
        }
    }

    class Aviary
    {
        private List<Animal> _animals = new List<Animal>();

        public Aviary(Animal animal)
        {
            AddAnimals(animal);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"В вольере {_animals.Count} животных.\n");

            for (int i = 0; i < _animals.Count; i++)
            {
                Console.Write($"{i + 1} - ");
                _animals[i].ShowInfo();
            }

            Console.WriteLine("\nПеред вами один из жителей вольера.");
            _animals.First().MakeSound();
        }

        private void AddAnimals(Animal animal)
        {
            int minimalCount = 1;
            int maximalCount = 7;
            int animalsCount;

            animalsCount = UserUtils.GenerateRandomNumber(minimalCount, maximalCount);

            for (int i = 0; i < animalsCount; i++)
            {
                _animals.Add(animal.Recreate());
            }
        }
    }

    abstract class Animal
    {
        protected string _name;
        protected string _gender;

        public Animal()
        {
            _gender = SetGender();
        }

        public abstract void MakeSound();

        public abstract Animal Recreate();

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Вид: {_name}. Пол - {_gender}");
        }

        private string SetGender()
        {
            string boyGender = "Самец";
            string girlGender = "Самка";

            return UserUtils.GenerateRandomNumber(0, 1) == 1 ? boyGender : girlGender;
        }
    }

    class Wolf : Animal
    {
        public Wolf()
        {
            _name = "Волк";
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{_name} воет.");
        }

        public override Animal Recreate()
        {
            return new Wolf();
        }
    }

    class Bear : Animal
    {
        public Bear()
        {
            _name = "Медведь";
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{_name} устало смотрит и молчит.");
        }

        public override Animal Recreate()
        {
            return new Bear();
        }
    }

    class Lion : Animal
    {
        public Lion()
        {
            _name = "Лев";
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{_name} издает громкий рык.");
        }

        public override Animal Recreate()
        {
            return new Lion();
        }
    }

    class Eagle : Animal
    {
        public Eagle()
        {
            _name = "Орёл";
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{_name} издаёт клёкот.");
        }

        public override Animal Recreate()
        {
            return new Eagle();
        }
    }

    class WildCat : Animal
    {
        public WildCat()
        {
            _name = "Рысь";
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{_name}, наевшись обедом, мурчит.");
        }

        public override Animal Recreate()
        {
            return new WildCat();
        }
    }

    class Snake : Animal
    {
        public Snake()
        {
            _name = "Змея";
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{_name} шипит.");
        }

        public override Animal Recreate()
        {
            return new Snake();
        }
    }

    class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return s_random.Next(min, max + 1);
        }
    }
}
