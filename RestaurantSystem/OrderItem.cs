using System;

namespace RestaurantSystem;

public class OrderItem
{
    public MenuItem Item { get; set; }
    public int Quantity { get; set; }

    public OrderItem(MenuItem item, int quantity)
    {
        Item = item ?? throw new ArgumentNullException(nameof(item));
        if (quantity <= 0)
            throw new ArgumentException("Кількість повинна бути ≥ 1", nameof(quantity));
        Quantity = quantity;
    }

    public decimal GetTotalPrice()
    {
        return Item.Price * Quantity;
    }

    public override string ToString()
    {
        return $"{Item.Name} × {Quantity} = {GetTotalPrice():C}";
    }
}