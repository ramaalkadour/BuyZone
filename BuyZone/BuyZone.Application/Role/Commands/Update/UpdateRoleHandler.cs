using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuyZone.Application.Role.Commands.Update;

public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand.Request, UpdateRoleCommand.Response>
{
    private readonly RoleManager<Domain.Entities.Security.Role> _roleManager;

    public UpdateRoleHandler(RoleManager<Domain.Entities.Security.Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<UpdateRoleCommand.Response> Handle(UpdateRoleCommand.Request request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.Id.ToString());
        if (role == null)
            throw new Exception("Role not found");

        role.Name = request.Name;
        var result = await _roleManager.UpdateAsync(role);

        if (!result.Succeeded)
            throw new Exception("Role update failed");

        return new UpdateRoleCommand.Response { Id = role.Id, Name = role.Name };
    }
}