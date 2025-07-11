using BuyZone.Domain.BaseEntity;

namespace BuyZone.Domain.Entities;

public class Order:IBaseEntity
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public double Price { get; set; }

    public Order( Guid customerId, Guid productId, double price)
    {
        CustomerId = customerId;
        ProductId = productId;
        Price = price;
    }
}