using System.Linq.Expressions;
using MediatR;

namespace BuyZone.Application.Order.Queries.GetAll;

public class GetAllOrdersQuery
{
    public class Request : IRequest<Response>
    {
        
    }

    public class Response
    {
        public int Count { get; set; }
        public List<OrdersRes> Orders { get; set; }

        public class OrdersRes
        {
            public Guid Id { get; set; }
            public Guid CustomerId { get; set; }
            public Guid ProductId { get; set; }
            public string ProductName { get; set; }
            public double Price { get; set; }

            public static Expression<Func<Domain.Entities.Order, OrdersRes>> Selector() => order => new OrdersRes
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                ProductId = order.ProductId,
                ProductName = order.Product.Name,
                Price = order.Price
            };
        }

    }
}