using System.Linq.Expressions;
using BuyZone.WAF.Domain.Enums;
using MediatR;

namespace BuyZone.WAF.Application.Logs.Queries.GetById;

public class GetLogByIdQuery
{
    public class Request:IRequest<Response>
    {
        public Guid Id { get; set; }
        
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string IpAddress { get; set; }
        public TypeOfAttack? TypeOfAttack { get; set; }
        public string Status { get; set; }
        public string Path { get; set; }
        public string Request { get; set; }
        public DateTime DateCreated { get; set; }

        public static Expression<Func<Domain.Entities.Logs, Response>> Selector() => l => new()
        {
            Id = l.Id,
            IpAddress = l.IpAddress,
            TypeOfAttack = l.TypeOfAttack,
            Status = l.Status,
            Request = l.Request,
            Path = l.Path,
            DateCreated = l.DateCreated
        };
    }
}