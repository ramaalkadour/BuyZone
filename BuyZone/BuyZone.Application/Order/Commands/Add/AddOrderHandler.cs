using BuyZone.Application.Order.Queries.GetAll;
using BuyZone.Domain;
using BuyZone.Domain.Entities;

using MediatR;

namespace BuyZone.Application.Order.Commands.Add;

public class AddOrderHandler : IRequestHandler<AddOrderCommand.Request, GetAllOrdersQuery.Response.OrdersRes>
{
    private readonly IRepository _orderRepository;

    public AddOrderHandler(IRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<GetAllOrdersQuery.Response.OrdersRes> Handle(AddOrderCommand.Request request, CancellationToken cancellationToken)
    {
        var price = 34;
        var order = new Domain.Entities.Order(
            customerId: request.CustomerId,
            productId: request.ProductId,
            price: price,
            quantity: request.Quantity
        );

        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();

        return new GetAllOrdersQuery.Response.OrdersRes
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            ProductId = order.ProductId,
            Price = order.Price
        };
    }
}