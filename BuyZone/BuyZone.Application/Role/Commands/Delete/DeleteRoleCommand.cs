using MediatR;

namespace BuyZone.Application.Role.Commands.Delete;

public class DeleteRoleCommand
{
    public class Request : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}