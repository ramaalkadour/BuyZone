using BuyZone.Application.Customer.Queries.GetAll;
using BuyZone.Domain.BaseUser;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuyZone.Application.Customer.Commands.Add;

public class AddCustomerHandler:IRequestHandler<AddCustomerCommand.Request,GetAllCustomerQuery.Response.CustomerRes>
{
    private readonly UserManager<User> _userManager;

    public AddCustomerHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<GetAllCustomerQuery.Response.CustomerRes> Handle(AddCustomerCommand.Request request, CancellationToken cancellationToken)
    {
        var customer = new Domain.Entities.Security.Customer(request.FirstName,request.LastName,request.Email,
        request.PhoneNumber,request.Address);
        var result = await _userManager.CreateAsync(customer, request.Password);
        if(!result.Succeeded)
            throw new Exception("Add Customer Is Failed");
        return new GetAllCustomerQuery.Response.CustomerRes()
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
        };
    }
}