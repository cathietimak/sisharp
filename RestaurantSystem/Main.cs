using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 1. Створення меню та додавання позицій
            var menu = new Menu();

            // Upcast: Dish/Beverage → MenuItem (неявно при передачі в AddItem)
            menu.AddItem(new Dish(1, "Борщ", 120, "Супи", "Основна страва", 25));
            menu.AddItem(new Dish(2, "Вареники з вишнями", 150, "Основні", "Десерт", 20));
            menu.AddItem(new Dish(3, "Олив'є", 90, "Салати", "Закуска", 5));
            menu.AddItem(new Beverage(4, "Чай з лимоном", 40, "Напої", 250, false));
            menu.AddItem(new Beverage(5, "Вино червоне", 220, "Алкоголь", 150, true));
            menu.AddItem(new Beverage(6, "Морс", 50, "Напої", 300, false));

            Console.WriteLine("=== ПОВНЕ МЕНЮ ===");
            menu.DisplayMenu();

            // 2. Пошук у меню (комбінований)
            Console.WriteLine("\n→ Комбінований пошук: назва 'морс' + категорія 'Напої':");
            var morse = menu.Search(nameQuery: "морс", category: "Напої");
            foreach (var item in morse)
                Console.WriteLine($"  {item.GetDetails()}");

            // 3. Створення системи замовлень
            var orderSystem = new OrderSystem(menu);

            // 4. Створення замовлень
            Console.WriteLine("\n=== РОБОТА З ЗАМОВЛЕННЯМИ ===");
            var order1 = orderSystem.CreateOrder(3);  // стіл №3
            var order2 = orderSystem.CreateOrder(5);  // стіл №5

            // Додавання позицій
            orderSystem.AddItemToOrder(order1.Id, 1, 2);  // 2 борщі
            orderSystem.AddItemToOrder(order1.Id, 4);     // 1 чай
            orderSystem.AddItemToOrder(order2.Id, 3);     // 1 олив'є
            orderSystem.AddItemToOrder(order2.Id, 5);     // 1 вино

            // Перегляд замовлень
            Console.WriteLine("\n→ Замовлення #1:");
            order1.DisplayInfo();

            Console.WriteLine("\n→ Замовлення #2:");
            order2.DisplayInfo();

            // 5. Пошук замовлення за ID
            Console.WriteLine("\n=== ПОШУК ЗАМОВЛЕННЯ ЗА ID ===");
            var foundOrder = orderSystem.GetOrderById(order1.Id);
            if (foundOrder != null)
            {
                Console.WriteLine($"Знайдено замовлення #{foundOrder.Id} для столика #{foundOrder.TableNumber}");
            }

            // 6. Зміна статусу
            Console.WriteLine("\n=== ЗМІНА СТАТУСУ ===");
            orderSystem.SetOrderStatus(order1.Id, OrderStatus.InProgress);
            orderSystem.SetOrderStatus(order1.Id, OrderStatus.Ready);
            orderSystem.SetOrderStatus(order1.Id, OrderStatus.Paid);  // оплата

            // Спроба додати після оплати 
            Console.WriteLine("\n→ Спроба додати до оплаченого замовлення:");
            orderSystem.AddItemToOrder(order1.Id, 6);  // морс — має відмовити

            // 7. Активні замовлення
            Console.WriteLine("\n=== АКТИВНІ ЗАМОВЛЕННЯ ===");
            orderSystem.DisplayActiveOrders();  // покаже лише замовлення #2

            // 8. Явний downcast (демонстрація)
            Console.WriteLine("\n=== DEMONSTRAЦІЯ UPCAST/DOWNCAST ===");
            var itemById = menu.GetItemById(5);  // Вино червоне (Beverage)
            if (itemById != null)
            {
                Console.WriteLine($"Upcast: MenuItem item = new Beverage(...) → тип: {itemById.GetType().Name}");

                // Downcast: спроба перетворити MenuItem на Beverage
                if (itemById is Beverage beverage)
                {
                    Console.WriteLine($"Downcast успішний: beverage.IsAlcoholic = {beverage.IsAlcoholic}");
                    Console.WriteLine($"  Об'єм: {beverage.Volume} мл, ціна: {beverage.Price:C}");
                }

                // Альтернативний downcast через 'as'
                var asDish = itemById as Dish;
                if (asDish == null)
                {
                    Console.WriteLine("Downcast до Dish провалився (as повернув null) — очікувано.");
                }
            }

            // 9. Видалення позиції
            Console.WriteLine("\n=== ВИДАЛЕННЯ ПОЗИЦІЇ ===");
            orderSystem.RemoveItemFromOrder(order2.Id, 3);  // видалити олив'є
            Console.WriteLine("→ Після видалення:");
            order2.DisplayInfo();

            // 10. Скасування замовлення
            Console.WriteLine("\n=== СКАСУВАННЯ ЗАМОВЛЕННЯ ===");
            orderSystem.CancelOrder(order2.Id);
            Console.WriteLine("→ Активні замовлення після скасування:");
            orderSystem.DisplayActiveOrders();

            Console.WriteLine("\n=== РОБОТУ ЗАВЕРШЕНО ===");
            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}