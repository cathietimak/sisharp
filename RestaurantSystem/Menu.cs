using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantSystem
{
    // Абстрактний базовий клас для позицій меню
    public abstract class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        protected MenuItem(int id, string name, decimal price, string category)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }

        public abstract string GetDetails();
    }

    // Клас для страв
    public class Dish : MenuItem
    {
        public string DishType { get; set; } // Закуска, основна страва, десерт
        public int PreparationTime { get; set; } // Час приготування в хвилинах

        public Dish(int id, string name, decimal price, string category, string dishType, int preparationTime)
            : base(id, name, price, category)
        {
            DishType = dishType;
            PreparationTime = preparationTime;
        }

        public override string GetDetails()
        {
            return $"[Страва] {Name} - {Price:C} | Тип: {DishType} | Категорія: {Category} | Час приготування: {PreparationTime} хв";
        }
    }

    // Клас для напоїв
    public class Beverage : MenuItem
    {
        public double Volume { get; set; } // Об'єм в мл
        public bool IsAlcoholic { get; set; }

        public Beverage(int id, string name, decimal price, string category, double volume, bool isAlcoholic)
            : base(id, name, price, category)
        {
            Volume = volume;
            IsAlcoholic = isAlcoholic;
        }

        public override string GetDetails()
        {
            string alcoholInfo = IsAlcoholic ? "Алкогольний" : "Безалкогольний";
            return $"[Напій] {Name} - {Price:C} | {alcoholInfo} | Об'єм: {Volume} мл | Категорія: {Category}";
        }
    }

    // Клас Меню ресторану
    public class Menu
    {
        private List<MenuItem> _items;

        public Menu()
        {
            _items = new List<MenuItem>();
        }

        // Додавання позиції до меню
        public void AddItem(MenuItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Позиція меню не може бути null");
            }

            if (_items.Any(i => i.Id == item.Id))
            {
                throw new InvalidOperationException($"Позиція з ID {item.Id} вже існує в меню");
            }

            _items.Add(item);
            Console.WriteLine($" Додано до меню: {item.Name}");
        }

        // Видалення позиції з меню
        public bool RemoveItem(int itemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                _items.Remove(item);
                Console.WriteLine($" Видалено з меню: {item.Name}");
                return true;
            }
            Console.WriteLine($" Позицію з ID {itemId} не знайдено");
            return false;
        }

        // Пошук позиції за ID
        public MenuItem GetItemById(int itemId)
        {
            return _items.FirstOrDefault(i => i.Id == itemId);
        }

        // Комбінований пошук: за назвою АБО категорією
        public List<MenuItem> Search(string nameQuery = null, string category = null)
        {
            var result = _items.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(nameQuery))
            {
                string nq = nameQuery.Trim().ToLowerInvariant();
                result = result.Where(item => item.Name != null && item.Name.ToLowerInvariant().Contains(nq));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                string nc = category.Trim().ToLowerInvariant();
                result = result.Where(item => item.Category != null && item.Category.ToLowerInvariant() == nc);
            }

            return result.ToList();
        }

        // Отримання всіх позицій
        public List<MenuItem> GetAllItems()
        {
            return new List<MenuItem>(_items);
        }

        // Отримання страв
        public List<Dish> GetDishes()
        {
            return _items.OfType<Dish>().ToList();
        }

        // Отримання напоїв
        public List<Beverage> GetBeverages()
        {
            return _items.OfType<Beverage>().ToList();
        }

        // Перегляд повного меню
        public void DisplayMenu()
        {
            if (_items.Count == 0)
            {
                Console.WriteLine("Меню порожнє");
                return;
            }

            Console.WriteLine("\n" + new string('=', 80));
            Console.WriteLine("                           МЕНЮ РЕСТОРАНУ");
            Console.WriteLine(new string('=', 80));

            // Відображення страв
            var dishes = GetDishes();
            if (dishes.Any())
            {
                Console.WriteLine("\n--- СТРАВИ ---");
                foreach (var dish in dishes.OrderBy(d => d.Category))
                {
                    Console.WriteLine(dish.GetDetails());
                }
            }

            // Відображення напоїв
            var beverages = GetBeverages();
            if (beverages.Any())
            {
                Console.WriteLine("\n--- НАПОЇ ---");
                foreach (var beverage in beverages.OrderBy(b => b.Category))
                {
                    Console.WriteLine(beverage.GetDetails());
                }
            }

            Console.WriteLine(new string('=', 80) + "\n");
        }

        // Кількість позицій у меню
        public int Count => _items.Count;
    }
}