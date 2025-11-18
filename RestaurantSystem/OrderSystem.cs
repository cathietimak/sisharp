namespace RestaurantSystem;

public class OrderSystem
{
    private List<Order> _orders = new List<Order>();
    private int _nextOrderId = 1;
    private Menu _menu;

    // Налаштування: діапазон дозволених номерів столиків
    private static readonly int MinTableNumber = 1;
    private static readonly int MaxTableNumber = 50;

    public OrderSystem(Menu menu)
    {
        _menu = menu ?? throw new ArgumentNullException(nameof(menu));
    }

    // Валідація номера столика
    private void ValidateTableNumber(int tableNumber)
    {
        if (tableNumber < MinTableNumber || tableNumber > MaxTableNumber)
        {
            throw new ArgumentOutOfRangeException(
                nameof(tableNumber),
                $"Номер столика має бути в діапазоні [{MinTableNumber}–{MaxTableNumber}]"
            );
        }
    }

    // Перевірка, чи є вже активне замовлення для столика
    private bool HasActiveOrderForTable(int tableNumber)
    {
        return _orders.Any(o => o.TableNumber == tableNumber && o.Status != OrderStatus.Paid);
    }

    // Створення нового замовлення
    public Order CreateOrder(int tableNumber)
    {
        ValidateTableNumber(tableNumber);

        if (HasActiveOrderForTable(tableNumber))
        {
            var activeOrder = _orders.First(o => o.TableNumber == tableNumber && o.Status != OrderStatus.Paid);
            throw new InvalidOperationException(
                $"Для столика #{tableNumber} вже існує активне замовлення (ID {activeOrder.Id}, статус: {activeOrder.Status}). " +
                "Спочатку завершіть або оплатіть його."
            );
        }

        var order = new Order(_nextOrderId++, tableNumber);
        _orders.Add(order);
        Console.WriteLine($" Створено замовлення #{order.Id} для столика #{tableNumber}");
        return order;
    }

    // Додавання позиції до замовлення
    public bool AddItemToOrder(int orderId, int menuItemId, int quantity = 1)
    {
        if (quantity <= 0)
        {
            Console.WriteLine(" Кількість має бути ≥ 1");
            return false;
        }

        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
        {
            Console.WriteLine($" Замовлення #{orderId} не знайдено");
            return false;
        }

        if (order.Status == OrderStatus.Paid)
        {
            Console.WriteLine($" Не можна додавати позиції до оплаченого замовлення #{orderId}");
            return false;
        }

        var item = _menu.GetItemById(menuItemId);
        if (item == null)
        {
            Console.WriteLine($" Позиція меню з ID {menuItemId} не знайдена");
            return false;
        }

        order.AddItem(item, quantity);
        Console.WriteLine($" Додано {item.Name} × {quantity} до замовлення #{orderId}");
        return true;
    }

    // Видалення позиції з замовлення
    public bool RemoveItemFromOrder(int orderId, int menuItemId, int quantity = -1)
    {
        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
        {
            Console.WriteLine($" Замовлення #{orderId} не знайдено");
            return false;
        }

        if (order.Status == OrderStatus.Paid)
        {
            Console.WriteLine($" Не можна видаляти позиції з оплаченого замовлення #{orderId}");
            return false;
        }

        var orderItem = order.GetItems().FirstOrDefault(oi => oi.Item.Id == menuItemId);
        if (orderItem == null)
        {
            Console.WriteLine($" Позиція з ID {menuItemId} відсутня в замовленні #{orderId}");
            return false;
        }

        if (quantity > 0 && quantity > orderItem.Quantity)
        {
            Console.WriteLine(
                $" У замовленні лише {orderItem.Quantity} шт. {orderItem.Item.Name}. " +
                $"Не можна видалити {quantity} шт."
            );
            return false;
        }

        order.RemoveItem(menuItemId, quantity);
        Console.WriteLine($" Видалено {(quantity == -1 ? "всі" : $"×{quantity}")} шт. ID {menuItemId} з замовлення #{orderId}");
        return true;
    }

    // Зміна статусу
    public bool SetOrderStatus(int orderId, OrderStatus status)
    {
        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
        {
            Console.WriteLine($" Замовлення #{orderId} не знайдено");
            return false;
        }

        if (status < order.Status)
        {
            Console.WriteLine($" Недопустима зміна статусу: '{order.Status}' → '{status}'. Статус може лише просуватися вперед.");
            return false;
        }

        try
        {
            order.SetStatus(status);
            Console.WriteLine($" Замовлення #{orderId} тепер у статусі: {status}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Помилка зміни статусу: {ex.Message}");
            return false;
        }
    }

    // Скасування замовлення (тільки для неоплачених)
    public bool CancelOrder(int orderId)
    {
        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
        {
            Console.WriteLine($" Замовлення #{orderId} не знайдено");
            return false;
        }

        if (order.Status == OrderStatus.Paid)
        {
            Console.WriteLine($" Не можна скасувати оплачене замовлення #{orderId}");
            return false;
        }

        _orders.Remove(order);
        Console.WriteLine($" Замовлення #{orderId} скасовано");
        return true;
    }

    // Публічні методи — без змін
    public Order GetOrderById(int orderId) => _orders.FirstOrDefault(o => o.Id == orderId);
    public List<Order> GetActiveOrders() => _orders.Where(o => o.Status != OrderStatus.Paid).ToList();

    public void DisplayActiveOrders()
    {
        var active = GetActiveOrders();
        Console.WriteLine($"\nАктивні замовлення ({active.Count}):");
        Console.WriteLine(new string('=', 60));

        if (!active.Any())
        {
            Console.WriteLine("  Немає активних замовлень.");
        }
        else
        {
            foreach (var order in active.OrderBy(o => o.TableNumber).ThenBy(o => o.CreatedAt))
            {
                order.DisplayInfo();
            }
        }

        Console.WriteLine(new string('=', 60) + "\n");
    }
}