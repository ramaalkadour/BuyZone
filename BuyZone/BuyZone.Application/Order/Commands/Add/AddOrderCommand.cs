using BuyZone.Application.Order.Queries.GetAll;
using BuyZone.Application.Order.Queries.GetById;
using MediatR;

namespace BuyZone.Application.Order.Commands.Add;

public class AddOrderCommand
{
    public class Request: IRequest<GetAllOrdersQuery.Response.OrdersRes>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}