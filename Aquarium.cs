namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int CommandToAddFish = 1;
            const int CommandToRemoveFish = 2;
            const int CommandToWatch = 3;
            const int CommandToExit = 4;

            bool isAquariumWork = true;
            int days = 0;

            Aquarium aquarium = new Aquarium();

            while (isAquariumWork)
            {
                Console.WriteLine($"{++days} дней работы аквариума.\n");
                aquarium.ShowInfo();

                Console.WriteLine("\nСписок команд:");
                Console.WriteLine($"{CommandToAddFish} - Добавить рыбу.");
                Console.WriteLine($"{CommandToRemoveFish} - Удалить рыбу.");
                Console.WriteLine($"{CommandToWatch} - Наблюдать (Ничего не делать).");
                Console.WriteLine($"{CommandToExit} - Выход.");

                Console.Write("Выберите команду: ");
                int.TryParse(Console.ReadLine(), out int userCommand);

                switch (userCommand)
                {
                    case CommandToAddFish:
                        aquarium.AddFish();
                        break;

                    case CommandToRemoveFish:
                        aquarium.RemoveFish();
                        break;

                    case CommandToWatch:
                        Console.WriteLine("Вы наблюдаете за аквариумом.");
                        break;

                    case CommandToExit:
                        isAquariumWork = false;
                        break;

                    default:
                        Console.WriteLine("Некорректная команда.");
                        break;
                }

                aquarium.Live();

                Console.WriteLine("Нажмите любую клавишу для продолжения: ");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Fish
    {
        private int _minimalAge;
        private int _maximalAge;

        public Fish()
        {
            _isAlive = true;
            _minimalAge = 1;
            _maximalAge = 10;
            Age = UserUtils.GenerateRandomNumber(_minimalAge, _maximalAge);
        }

        public int Age { get; private set; }
        public bool _isAlive { get; private set; }

        public void TrackHealth()
        {
            if (Age >= _maximalAge)
            {
                _isAlive = false;
            }
        }

        public void GrowUp()
        {
            if (_isAlive)
            {
                Age++;
            }
        }
    }

    class Aquarium
    {
        private int _capacity;
        private List<Fish> _fishes;

        public Aquarium()
        {
            _capacity = 10;
            _fishes = new List<Fish>
            {
                new Fish(), new Fish(), new Fish()
            };
        }

        public void AddFish()
        {
            if (_fishes.Count < _capacity)
            {
                _fishes.Add(new Fish());

                Console.WriteLine("Рыба добавлена.");
            }
            else
            {
                Console.WriteLine("Невозможно добавить новую рыбу. Аквариум полный");
            }
        }

        public void RemoveFish()
        {
            Console.Write("Введите номер рыбы для удаления: ");

            int.TryParse(Console.ReadLine(), out int userInput);
            userInput--;

            if (userInput >= 0 && userInput < _fishes.Count)
            {
                _fishes.RemoveAt(userInput);
            }
            else
            {
                Console.WriteLine("Некорректные данные ввода. Попробуйте позже.");
            }
        }

        public void Live()
        {
            foreach (Fish fish in _fishes)
            {
                fish.TrackHealth();
                fish.GrowUp();
            }
        }

        public void ShowInfo()
        {
            string status;
            string statusAlive = "Живая";
            string statusDead = "Мертвая";

            for (int i = 0; i < _fishes.Count; i++)
            {
                if (_fishes[i]._isAlive)
                {
                    status = statusAlive;
                }
                else
                {
                    status = statusDead;
                }

                Console.WriteLine($"{i + 1} - Возраст рыбы: {_fishes[i].Age}. Состояние: {status}.");
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
