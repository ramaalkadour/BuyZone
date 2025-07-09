using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuyZone.Application.Role.Commands.Add;

public class AddRoleHandler : IRequestHandler<AddRoleCommand.Request, AddRoleCommand.Response>
{
    private readonly RoleManager<Domain.Entities.Security.Role> _roleManager;
    public AddRoleHandler(RoleManager<Domain.Entities.Security.Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<AddRoleCommand.Response> Handle(AddRoleCommand.Request request, CancellationToken cancellationToken)
    {
        var role = new Domain.Entities.Security.Role { Name = request.Name };
        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
            throw new Exception("Role creation failed");

        return new AddRoleCommand.Response
        {
            Id = role.Id,
            Name = role.Name
        };
    }
}