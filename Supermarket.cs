namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bread bread = new Bread();
            Eggs eggs = new Eggs();
            Cheese cheese = new Cheese();
            Chicken cheken = new Chicken();
            Tomato tomato = new Tomato();
            Potato potato = new Potato();
            Cucumber cucumber = new Cucumber();
            Meat meat = new Meat();
            Butter butter = new Butter();
            Milk milk = new Milk();
            Apples apples = new Apples();
            Bananas bananas = new Bananas();
            Carrot carrot = new Carrot();
            Onions onions = new Onions();
            Fish fish = new Fish();
            Chocolate chocolate = new Chocolate();
            ChocolateBar chocolateBar = new ChocolateBar();
            Cola cola = new Cola();
            Chips chips = new Chips();
            Juice juice = new Juice();

            List<Product> products = new List<Product>()
            {
                bread, eggs, cheese, cheken, tomato, potato, cucumber, meat, butter, milk, apples, bananas, carrot, onions, fish, chocolate, chocolateBar, cola, chips, juice
            };

            Supermarket supermarket = new Supermarket(products);

            supermarket.Open();
        }
    }

    abstract class Product
    {
        public string Name { get; protected set; }
        public int Price { get; protected set; }

        public abstract Product Clone();

        public virtual void ShowInfo()
        {
            Console.WriteLine($"{Name} стоит {Price} рублей.");
        }
    }

    class Bread : Product
    {
        public Bread()
        {
            Name = "Хлеб";
            Price = 45;
        }

        public override Product Clone()
        {
            return new Bread();
        }
    }

    class Eggs : Product
    {
        public Eggs()
        {
            Name = "Яйца";
            Price = 110;
        }

        public override Product Clone()
        {
            return new Eggs();
        }
    }

    class Cheese : Product
    {
        public Cheese()
        {
            Name = "Сыр";
            Price = 160;
        }

        public override Product Clone()
        {
            return new Cheese();
        }
    }

    class Chicken : Product
    {
        public Chicken()
        {
            Name = "Курица (1 кг)";
            Price = 480;
        }

        public override Product Clone()
        {
            return new Chicken();
        }
    }

    class Tomato : Product
    {
        public Tomato()
        {
            Name = "Помидоры (1 кг)";
            Price = 170;
        }

        public override Product Clone()
        {
            return new Tomato();
        }
    }

    class Potato : Product
    {
        public Potato()
        {
            Name = "Картошка (1 кг)";
            Price = 40;
        }

        public override Product Clone()
        {
            return new Potato();
        }
    }

    class Cucumber : Product
    {
        public Cucumber()
        {
            Name = "Огурцы (1 кг)";
            Price = 120;
        }

        public override Product Clone()
        {
            return new Cucumber();
        }
    }

    class Meat : Product
    {
        public Meat()
        {
            Name = "Мясо (1 кг)";
            Price = 520;
        }

        public override Product Clone()
        {
            return new Meat();
        }
    }

    class Butter : Product
    {
        public Butter()
        {
            Name = "Сливочное масло";
            Price = 180;
        }

        public override Product Clone()
        {
            return new Butter();
        }
    }

    class Milk : Product
    {
        public Milk()
        {
            Name = "Молоко";
            Price = 80;
        }

        public override Product Clone()
        {
            return new Milk();
        }
    }

    class Apples : Product
    {
        public Apples()
        {
            Name = "Яблоки (1 кг)";
            Price = 130;
        }

        public override Product Clone()
        {
            return new Apples();
        }
    }

    class Bananas : Product
    {
        public Bananas()
        {
            Name = "Бананы (1 кг)";
            Price = 160;
        }

        public override Product Clone()
        {
            return new Bananas();
        }
    }

    class Carrot : Product
    {
        public Carrot()
        {
            Name = "Морковь (1 кг)";
            Price = 50;
        }

        public override Product Clone()
        {
            return new Carrot();
        }
    }

    class Onions : Product
    {
        public Onions()
        {
            Name = "Лук (кг)";
            Price = 45;
        }

        public override Product Clone()
        {
            return new Onions();
        }
    }

    class Fish : Product
    {
        public Fish()
        {
            Name = "Рыба (кг)";
            Price = 700;
        }

        public override Product Clone()
        {
            return new Fish();
        }
    }

    class Chocolate : Product
    {
        public Chocolate()
        {
            Name = "Шоколад";
            Price = 110;
        }

        public override Product Clone()
        {
            return new Chocolate();
        }
    }

    class ChocolateBar : Product
    {
        public ChocolateBar()
        {
            Name = "Шоколадный батончик";
            Price = 70;
        }

        public override Product Clone()
        {
            return new ChocolateBar();
        }
    }

    class Cola : Product
    {
        public Cola()
        {
            Name = "Кола";
            Price = 120;
        }

        public override Product Clone()
        {
            return new Cola();
        }
    }

    class Chips : Product
    {
        public Chips()
        {
            Name = "Чипсы";
            Price = 160;
        }

        public override Product Clone()
        {
            return new Chips();
        }
    }

    class Juice : Product
    {
        public Juice()
        {
            Name = "Сок";
            Price = 160;
        }

        public override Product Clone()
        {
            return new Juice();
        }
    }

    class Supermarket
    {
        private Queue<Buyer> _buyers = new Queue<Buyer>();
        private List<Product> _products;
        private int _buyersCount;
        private int _minimalBuyersCount;
        private int _maximalBuyersCount;

        public Supermarket(List<Product> assortment)
        {
            _products = assortment;
            _minimalBuyersCount = 3;
            _maximalBuyersCount = 7;
            _buyersCount = UserUtils.GenerateRandomNumber(_minimalBuyersCount, _maximalBuyersCount);
            CreateQueue();
        }

        public void Open()
        {
            Console.WriteLine("Добро пожаловать в магазин!\n");

            ShowAssortment();

            while (_buyers.Count > 0)
            {
                Console.WriteLine($"\nОсталось покупателей: {_buyers.Count}\n");

                Buyer currentBuyer = _buyers.Dequeue();

                Console.WriteLine($"Деньги покупателя: {currentBuyer.Money}\n");

                foreach (Product product in _products)
                {
                    if (currentBuyer.IsProductNeed() == true)
                    {
                        currentBuyer.AddProduct(product);
                    }
                }

                currentBuyer.ShowProductCart();
                currentBuyer.BuyProducts();
                currentBuyer.ShowInfo();

                Console.ReadLine();
            }

            Console.WriteLine("Клиенты закончились. Магазин закрывается.");
        }

        private void ShowAssortment()
        {
            foreach (Product product in _products)
            {
                product.ShowInfo();
            }
        }

        private void CreateQueue()
        {
            for (int i = 0; i < _buyersCount; i++)
            {
                _buyers.Enqueue(new Buyer());
            }
        }
    }

    class Buyer
    {
        private int _minimalMoney;
        private int _maximalMoney;
        private int _minimalCountOfProducts;
        private int _maximalCountOfProducts;
        private int _shoppingCartPrice;
        private int _chanceToBuy;
        private int _minimalChanceToBuy;
        private int _maximalChanceToBuy;
        private List<Product> _bag = new List<Product>();
        private List<Product> _shoppingCart = new List<Product>();

        public Buyer()
        {
            _minimalMoney = 500;
            _maximalMoney = 10000;
            Money = UserUtils.GenerateRandomNumber(_minimalMoney, _maximalMoney);
            _minimalCountOfProducts = 1;
            _maximalCountOfProducts = 5;
            _shoppingCartPrice = 0;
            _chanceToBuy = 33;
            _minimalChanceToBuy = 0;
            _maximalChanceToBuy = 100;
        }

        public int Money { get; private set; }

        public bool IsProductNeed()
        {
            int number = UserUtils.GenerateRandomNumber(_minimalChanceToBuy, _maximalChanceToBuy);

            if (number <= _chanceToBuy)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddProduct(Product product)
        {
            int countProducts = UserUtils.GenerateRandomNumber(_minimalCountOfProducts, _maximalCountOfProducts);

            for (int i = 0; i < countProducts; i++)
            {
                _shoppingCart.Add(product.Clone());

                _shoppingCartPrice += product.Price;
            }
        }

        public void ShowProductCart()
        {
            Console.WriteLine("Товары в корзине: ");

            Dictionary<string, int> products = CreateUniqProductsList(_shoppingCart);

            foreach (KeyValuePair<string, int> pair in products)
            {
                Console.WriteLine($"Товар: {pair.Key}. Цена: {pair.Value}. Количество: {_shoppingCart.Count(count => count.Name == pair.Key)}");
            }

            Console.WriteLine($"Стоимость корзины: {_shoppingCartPrice}\n");
            Console.Write("Нажмите любую клавишу для покупки: ");
            Console.ReadLine();
        }

        public void BuyProducts()
        {
            while (Money < _shoppingCartPrice && _shoppingCart.Count > 0)
            {
                int randomProduct = UserUtils.GenerateRandomNumber(0, _shoppingCart.Count - 1);
                Product removedProduct = _shoppingCart[randomProduct];

                Console.WriteLine($"Не хватает денег. Покупатель убирает {removedProduct.Name}");

                _shoppingCart.RemoveAt(randomProduct);

                _shoppingCartPrice -= removedProduct.Price;
            }

            foreach (Product product in _shoppingCart)
            {
                _bag.Add(product);

                Money -= product.Price;
            }

            _shoppingCart.Clear();

            _shoppingCartPrice = 0;
        }

        public void ShowInfo()
        {
            Console.WriteLine("Товары в сумке: ");

            Dictionary<string, int> products = CreateUniqProductsList(_bag);

            foreach (KeyValuePair<string, int> pair in products)
            {
                Console.WriteLine($"{pair.Key} - {_bag.Count(count => count.Name == pair.Key)}");
            }

            Console.WriteLine($"Осталось денег: {Money}\n");
        }

        private Dictionary<string, int> CreateUniqProductsList(List<Product> unsortedProducts)
        {
            Dictionary<string, int> products = new Dictionary<string, int>();

            foreach (Product product in unsortedProducts)
            {
                if (products.ContainsKey(product.Name) == false)
                {
                    products.Add(product.Name, product.Price);
                }
            }

            return products;
        }
    }

    class UserUtils
    {
        private static Random s_randomNumber = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return s_randomNumber.Next(min, max + 1);
        }
    }
}
