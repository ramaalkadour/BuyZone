using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Product.Queries.GetAll;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery.Request, GetAllProductsQuery.Response>
{
    private readonly IRepository _repository;

    public GetAllProductsHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetAllProductsQuery.Response> Handle(GetAllProductsQuery.Request request, CancellationToken cancellationToken)
    {
        var products = await _repository.Query<Domain.Entities.Product>()
            .Include(p=>p.Orders)
            .Select(GetAllProductsQuery.Response.ProductRes.Selector())
            .ToListAsync(cancellationToken);

        return new GetAllProductsQuery.Response
        {
            Count = products.Count,
            Products = products
        };
    }
}