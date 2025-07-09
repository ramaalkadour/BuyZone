using BuyZone.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Customer.Queries.GetById;

public class GetByIdCustomerHandler:IRequestHandler<GetByIdCustomerQuery.Request, GetByIdCustomerQuery.Response>
{
    private readonly IRepository _repository;

    public GetByIdCustomerHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<GetByIdCustomerQuery.Response> Handle(GetByIdCustomerQuery.Request request, CancellationToken cancellationToken)
    {
        var customer =await _repository.Query<Domain.Entities.Security.Customer>()
            .Where(c => c.Id == request.Id)
            .Select(GetByIdCustomerQuery.Response.Selector())
            .FirstAsync(cancellationToken);
        return customer;
    }
}