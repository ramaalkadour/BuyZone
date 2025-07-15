using System.Linq.Expressions;
using MediatR;
using System;
using System.Collections.Generic;

namespace BuyZone.WAF.Application.Logs.Queries.GetAll
{
    public class GetAllLogsQuery
    {
        public class Request : IRequest<Response> { public int PageSize { get; set; } = 10;
            public int PageIndex { get; set; } = 1;}

        public class Response
        {
            public List<LogDto> Logs { get; set; }
            public class LogDto
            {
                public Guid Id { get; set; }
                public string IpAddress { get; set; }
                public string Status { get; set; }
                public string DateCreated { get; set; }
                public static Expression<Func<Domain.Entities.Logs, LogDto>> Selector() => e => new LogDto
                {
                    Id = e.Id,
                    IpAddress = e.IpAddress,
                    Status = e.Status,
                    DateCreated = e.DateCreated.ToString("yyyy-MM-dd HH:mm:ss")
                };
            }
        }
    }
}