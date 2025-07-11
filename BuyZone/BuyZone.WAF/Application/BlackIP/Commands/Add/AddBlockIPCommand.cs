using BuyZone.WAF.Domain.Enums;
using MediatR;

namespace BuyZone.WAF.Application.BlackIP.Commands.Add;

public class AddBlockIPCommand
{
    public class Request:IRequest
    {
        public string IpAddress { get; set; }
        public IpStatus Status { get; set; }
    }
}