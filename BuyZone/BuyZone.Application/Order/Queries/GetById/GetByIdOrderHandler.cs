using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Order.Queries.GetById;

public class GetByIdOrderHandler:IRequestHandler<GetByIdOrderQuery.Request, GetByIdOrderQuery.Response>
{
    private readonly IRepository _repository;

    public GetByIdOrderHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<GetByIdOrderQuery.Response> Handle(GetByIdOrderQuery.Request request, CancellationToken cancellationToken)
    {
     var order=await _repository.Query<Domain.Entities.Order>()
         .Where(c => c.Id == request.Id)
         .Select(GetByIdOrderQuery.Response.Selector())
         .FirstAsync(cancellationToken);
     return order;
         
    }
}