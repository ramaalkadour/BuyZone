using BuyZone.Application.Order.Queries.GetAll;
using BuyZone.Application.Order.Queries.GetById;
using MediatR;

namespace BuyZone.Application.Order.Commands.Add;

public class AddOrderCommand
{
    public class Request: IRequest<GetAllOrdersQuery.Response.OrdersRes>, IRequest<GetAllOrdersQuery.Response>
    {
       
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public double Price { get; set; }
    }
}