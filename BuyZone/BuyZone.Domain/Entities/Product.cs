using BuyZone.Domain.BaseEntity;

namespace BuyZone.Domain.Entities;

public class Product:IBaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    public Product(Guid id, string name, string imageUrl, string description, double price, Guid categoryId)
    {
        Id = id;
        Name = name;
        ImageUrl = imageUrl;
        Description = description;
        Price = price;
        CategoryId = categoryId;
    }
}