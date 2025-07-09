using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BuyZone.Application.Order.Queries.GetAll;


namespace BuyZone.Application.Order.Queries.GetAll;

public class GetAllOrdersHandler:IRequestHandler<GetAllOrdersQuery.Request, GetAllOrdersQuery.Response>
{
    private readonly IRepository _repository;

    public GetAllOrdersHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<GetAllOrdersQuery.Response> Handle(GetAllOrdersQuery.Request request, CancellationToken cancellationToken)
    {
        var orders = await _repository.Query<Domain.Entities.Order>()
            .Include(o => o.Product)
            .Select(GetAllOrdersQuery.Response.OrdersRes.Selector())
            .ToListAsync(cancellationToken);

        return new GetAllOrdersQuery.Response
        {
            Count = orders.Count,
            Orders = orders
        };
    }
}