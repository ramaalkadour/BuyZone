using System.Linq.Expressions;
using MediatR;

namespace BuyZone.Application.Product.Queries.GetById;

public class GetByIdProductQuery
{
    public class Request : IRequest<Response>
    {
        public Guid Id { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }

        public static Expression<Func<Domain.Entities.Product, Response>> Selector() => e => new Response
        {
            Id = e.Id,
            Name = e.Name,
            ImageUrl = e.ImageUrl,
            Description = e.Description,
            Price = e.Price,
            CategoryId = e.CategoryId
        };
    }
}