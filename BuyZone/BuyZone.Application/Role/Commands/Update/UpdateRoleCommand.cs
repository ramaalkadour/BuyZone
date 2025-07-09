using MediatR;

namespace BuyZone.Application.Role.Commands.Update;

public class UpdateRoleCommand
{
    public class Request : IRequest<Response>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}