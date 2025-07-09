using System.Linq.Expressions;

namespace BuyZone.Application.Category.Commands.Update;

public class UpdateCategoryCommand
{
    public class Request
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}