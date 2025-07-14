using BuyZone.Domain.BaseEntity;

namespace BuyZone.Domain.Entities;

public class Product:IBaseEntity
{
    public Product() {}
    public Guid Id { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    private readonly List<Order> _orders = new();
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

    public Product(string name, string imageUrl, string description, double price, Guid categoryId)
    {
        Name = name;
        ImageUrl = imageUrl;
        Description = description;
        Price = price;
        CategoryId = categoryId;
    }
}