using BuyZone.Domain.BaseEntity;

namespace BuyZone.Domain.Entities;

public class Order:IBaseEntity
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public string ProductName { get; set; }

    public double Price { get; set; }

    public Order(Guid id, Guid customerId, Guid productId, double price, string productName)
    {
        Id = id;
        CustomerId = customerId;
        ProductId = productId;
        ProductName = productName;
        Price = price;
    }
}