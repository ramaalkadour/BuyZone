using MediatR;

namespace BuyZone.Application.Role.Queries.GetAll;

public class GetAllRolesQuery
{
    public class Request : IRequest<Response> {}

    public class Response
    {
        public int Count { get; set; }
        public List<RoleDto> Roles { get; set; } = new();

        public class RoleDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}