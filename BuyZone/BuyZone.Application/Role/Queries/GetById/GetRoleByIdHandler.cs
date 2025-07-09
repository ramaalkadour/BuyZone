using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuyZone.Application.Role.Queries.GetById;

public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery.Request, GetRoleByIdQuery.Response>
{
    private readonly RoleManager<Domain.Entities.Security.Role> _roleManager;
    public GetRoleByIdHandler(RoleManager<Domain.Entities.Security.Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<GetRoleByIdQuery.Response> Handle(GetRoleByIdQuery.Request request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.Id.ToString());
        if (role == null) throw new Exception("Role not found");

        return new GetRoleByIdQuery.Response { Id = role.Id, Name = role.Name };
    }
}