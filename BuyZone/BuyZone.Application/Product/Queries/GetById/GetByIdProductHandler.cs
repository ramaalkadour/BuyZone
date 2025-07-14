using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Product.Queries.GetById;

public class GetByIdProductHandler : IRequestHandler<GetByIdProductQuery.Request, GetByIdProductQuery.Response>
{
    private readonly IRepository _repository;

    public GetByIdProductHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetByIdProductQuery.Response> Handle(GetByIdProductQuery.Request request, CancellationToken cancellationToken)
    {
        var product = await _repository.Query<Domain.Entities.Product>()
            .Include(e=>e.Orders)
            .Include(e=>e.Category)
            .Where(p => p.Id == request.Id)
            .Select(GetByIdProductQuery.Response.Selector())
            .FirstOrDefaultAsync(cancellationToken);

        return product!;
    }
}