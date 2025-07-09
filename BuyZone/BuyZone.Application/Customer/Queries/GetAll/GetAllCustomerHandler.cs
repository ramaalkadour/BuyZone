using BuyZone.Domain;
using BuyZone.Domain.BaseUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Customer.Queries.GetAll;

public class GetAllCustomerHandler:IRequestHandler<GetAllCustomerQuery.Request,GetAllCustomerQuery.Response>
{
    private readonly IRepository _repository;

    public GetAllCustomerHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetAllCustomerQuery.Response> Handle(GetAllCustomerQuery.Request request, CancellationToken cancellationToken)
    {
        var customers = await _repository.Query<Domain.Entities.Security.Customer>()
            .Select(GetAllCustomerQuery.Response.CustomerRes.Selector())
            .ToListAsync(cancellationToken);
        return new GetAllCustomerQuery.Response()
        {
            Count = customers.Count,
            Customers = customers
        };


    }
}