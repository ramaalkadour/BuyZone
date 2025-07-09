using System.Linq.Expressions;
using MediatR;

namespace BuyZone.Application.Category.Queries.GetAll;

public class GetAllCategoriesQuery
{
    public class Request:IRequest<Response>
    {
        
    }

    public class Response
    {
        public int Count { get; set; }
        public List<CategoryDto> Categories { get; set; } = new();
        public class CategoryDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public static Expression<Func<Domain.Entities.Category, CategoryDto>> Selector() => c => new()
            {
                Id=c.Id,
                Name = c.Name
            };
        }
    }
}