using BuyZone.Domain.BaseEntity;

namespace BuyZone.Domain.Entities;

public class Category:IBaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    private readonly List<Product> _products = new List<Product>();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public Category(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}