using BuyZone.WAF.Domain.Enums;
using MediatR;

namespace BuyZone.WAF.Application.BlackIP.Commands.UpdateStatus;

public class UpdateStatusCommand
{
    public class Request:IRequest
    {
        public Guid Id { get; set; }
        public IpStatus Status { get; set; }
    }
}