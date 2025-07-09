using BuyZone.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuyZone.Application.Customer.Queries.GetById;

public class GetByIdCustomerHandler:IRequestHandler<GetByIdCustomerQuery.Request, GetByIdCustomerQuery.Response>
{
    private readonly IRepository _repository;

    public GetByIdCustomerHandler(IRepository repository)
    {
        _repository = repository;
    }
    public GetByIdCustomerQuery.Response Handle(GetByIdCustomerQuery.Request request, CancellationToken cancellationToken)
    {
        var customer = _repository.Query<Domain.Entities.Security.Customer>()
            .Where(c => c.Id == request.Id)
            .Select(GetByIdCustomerQuery.Response.Selector())
            .FirstOrDefault(cancellationToken);

        if (customer == null)
            return null;

        return new GetByIdCustomerQuery.Response
        {
            Customer = customer
        };
    }
}
}