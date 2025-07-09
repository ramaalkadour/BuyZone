using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuyZone.Application.Role.Commands.Delete;

public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand.Request, bool>
{
    private readonly RoleManager<Domain.Entities.Security.Role> _roleManager;

    public DeleteRoleHandler(RoleManager<Domain.Entities.Security.Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<bool> Handle(DeleteRoleCommand.Request request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.Id.ToString());
        if (role == null)
            return false;

        var result = await _roleManager.DeleteAsync(role);
        return result.Succeeded;
    }
}