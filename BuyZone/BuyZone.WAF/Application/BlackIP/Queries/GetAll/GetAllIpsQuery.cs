using System.Linq.Expressions;
using System.Net.NetworkInformation;
using BuyZone.WAF.Domain.Enums;
using MediatR;

namespace BuyZone.WAF.Application.BlackIP.Queries.GetAll;

public class GetAllIpsQuery
{
    public class Request:IRequest<Response>
    {
        public IpStatus ?Status { get; set; }
    }

    public class Response
    {
        public List<IpDto> Ips { get; set; }
        public class IpDto
        {
            public Guid Id { get; set; }
            public int Number { get; set; }
            public string IpAddress { get; set; }
            public long NumberOfRequests { get; set; }
            public string FirstSeen { get; set; }
            public string LastSeen { get; set; }
            public int BlockCount { get; set; }
            public IpStatus Status { get; set; }
            public static Expression<Func<Domain.Entities.BlockIP, IpDto>> Selector() => e => new IpDto
            {
                Id = e.Id,
                Number = e.Number,
                IpAddress = (e.IpAddress=="::1"?"localhost":e.IpAddress),
                NumberOfRequests = e.NumberOfRequests,
                Status = e.Status,
                FirstSeen = e.FirstSeen.ToString("yyyy-MM-dd HH:mm:ss"),
                LastSeen = e.LastSeen.ToString("yyyy-MM-dd HH:mm:ss"),
                BlockCount = e.BlockedCount
            };
        }
    }
}