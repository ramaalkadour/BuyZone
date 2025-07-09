using MediatR;

namespace BuyZone.Application.Role.Queries.GetById;

public class GetRoleByIdQuery
{
    public class Request : IRequest<Response>
    {
        public Guid Id { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}