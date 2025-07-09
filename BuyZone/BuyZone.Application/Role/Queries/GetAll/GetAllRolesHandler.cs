using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuyZone.Application.Role.Queries.GetAll;

public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery.Request, GetAllRolesQuery.Response>
{
    private readonly RoleManager<Domain.Entities.Security.Role> _roleManager;
    public GetAllRolesHandler(RoleManager<Domain.Entities.Security.Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<GetAllRolesQuery.Response> Handle(GetAllRolesQuery.Request request, CancellationToken cancellationToken)
    {
        var roles =  _roleManager.Roles.Select(r => new GetAllRolesQuery.Response.RoleDto
        {
            Id = r.Id,
            Name = r.Name??""
        }).ToList();

        return new GetAllRolesQuery.Response
        {
            Count=roles.Count,
            Roles = roles
        };
    }
}