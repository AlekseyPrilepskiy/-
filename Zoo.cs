namespace ConsoleApp32
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AnimalsGenerator animalsGenerator = new AnimalsGenerator();
            AviariesGenerator aviariesGenerator = new AviariesGenerator(animalsGenerator);

            Zoo zoo = new Zoo(aviariesGenerator);
            zoo.TakeTour();
        }
    }

    class Animal
    {
        private string _name;
        private string _sound;
        private string _gender;

        public Animal(string name, string sound)
        {
            _name = name;
            _sound = sound;
            _gender = GenerateGender();
        }

        public void MakeSound()
        {
            Console.WriteLine($"{_name} издает звук: {_sound}");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Вид: {_name}. Пол - {_gender}");
        }

        private string GenerateGender()
        {
            string boyGender = "Самец";
            string girlGender = "Самка";
            List<string> genders = new List<string>() { boyGender, girlGender };

            return UserUtils.GenerateRandomNumber(0, genders.Count) == (genders.Count - 1) ? boyGender : girlGender;
        }
    }

    class AnimalsGenerator
    {
        private List<string> _names;
        private List<string> _sounds;

        public AnimalsGenerator()
        {
            _names = new List<string> { "Волк", "Лев", "Рысь", "Собака", "Орел", "Змея" };
            _sounds = new List<string> { "Воет", "Рычит", "Мурчит", "Лает", "Клекочет", "Шипит" };
        }

        public List<Animal> Generate()
        {
            int randomIndex = UserUtils.GenerateRandomNumber(0, _names.Count);
            int minimalCount = 2;
            int maximalCount = 8;
            int count = UserUtils.GenerateRandomNumber(minimalCount, maximalCount);

            List<Animal> animals = new List<Animal>();

            for (int i = 0; i < count; i++)
            {
                animals.Add(new Animal(_names[randomIndex], _sounds[randomIndex]));
            }

            return animals;
        }
    }

    class Aviary
    {
        private List<Animal> _animals;

        public Aviary(List<Animal> animals)
        {
            _animals = new List<Animal>(animals);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Всего {_animals.Count} животных в вольере.\n");

            for (int i = 0; i < _animals.Count; i++)
            {
                Console.Write($"{i + 1} - ");
                _animals[i].ShowInfo();
            }

            Console.WriteLine("\nПрямо перед вами находится один из жителей вольера.");
            _animals.First().MakeSound();
        }
    }

    class AviariesGenerator
    {
        private AnimalsGenerator _animalsGenerator;

        public AviariesGenerator(AnimalsGenerator animalsGenerator)
        {
            _animalsGenerator = animalsGenerator;
        }

        public List<Aviary> Generate()
        {
            int minimalCount = 4;
            int maximalCount = 9;
            int count = UserUtils.GenerateRandomNumber(minimalCount, maximalCount);

            List<Aviary> aviaries = new List<Aviary>();

            for (int i = 0; i < count; i++)
            {
                aviaries.Add(new Aviary(_animalsGenerator.Generate()));
            }

            return aviaries;
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries;

        public Zoo(AviariesGenerator aviariesGenerator)
        {
            _aviaries = new List<Aviary>(aviariesGenerator.Generate());
        }

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
                }
                else if (userInput == CommandToExit)
                {
                    isOpen = false;
                }
                else
                {
                    Console.WriteLine("Неверный ввод пользователя. Попробуйте снова.");
                }

                Console.ReadKey();
                Console.Clear();
            }
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
