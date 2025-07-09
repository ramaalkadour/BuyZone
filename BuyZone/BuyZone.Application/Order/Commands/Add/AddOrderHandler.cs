using BuyZone.Application.Order.Queries.GetAll;
using BuyZone.Domain.BaseUser;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuyZone.Application.Order.Commands.Add;

public class AddOrderHandler:IRequestHandler<AddOrderCommand.Request, GetAllOrdersQuery.Response.OrdersRes>
{
    private readonly UserManager<User> _userManager;

    public AddOrderHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public Task<GetAllOrdersQuery.Response.OrdersRes> Handle(AddOrderCommand.Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}