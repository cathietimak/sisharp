using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantSystem;

public class Order
{
    public int Id { get; }
    public int TableNumber { get; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public OrderStatus Status { get; private set; } = OrderStatus.New;

    private readonly List<OrderItem> _items = new();

    public Order(int id, int tableNumber)
    {
        Id = id;
        TableNumber = tableNumber;
    }

    public void AddItem(MenuItem item, int quantity = 1)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        if (quantity <= 0) throw new ArgumentException("Кількість повинна бути ≥ 1", nameof(quantity));

        var existing = _items.FirstOrDefault(oi => oi.Item.Id == item.Id);
        if (existing != null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            _items.Add(new OrderItem(item, quantity));
        }
    }

    public bool RemoveItem(int itemId, int quantity = -1)
    {
        var orderItem = _items.FirstOrDefault(oi => oi.Item.Id == itemId);
        if (orderItem == null) return false;

        if (quantity == -1 || quantity >= orderItem.Quantity)
        {
            _items.Remove(orderItem);
        }
        else
        {
            orderItem.Quantity -= quantity;
        }
        return true;
    }

    public decimal GetTotalPrice()
    {
        return _items.Sum(oi => oi.GetTotalPrice());
    }

    public void SetStatus(OrderStatus newStatus)
    {
        if (newStatus < Status)
        {
            throw new InvalidOperationException($"Не можна змінити статус із '{Status}' на '{newStatus}'");
        }
        Status = newStatus;
    }

    public IReadOnlyList<OrderItem> GetItems()
    {
        return _items.AsReadOnly();
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"\nЗамовлення #{Id} (Стіл #{TableNumber}) | Статус: {Status} | Створено: {CreatedAt:HH:mm dd.MM.yyyy}");
        Console.WriteLine(new string('-', 50));

        if (_items.Count == 0)
        {
            Console.WriteLine("  Позицій немає.");
        }
        else
        {
            foreach (var item in _items)
            {
                Console.WriteLine($"  {item}");
            }
        }

        Console.WriteLine($"  Загалом: {GetTotalPrice():C}");
        Console.WriteLine(new string('-', 50));
    }
}