using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace BuyZone.WAF.Application.Logs.Queries.GetAll
{
    public class GetAllLogsHandler : IRequestHandler<GetAllLogsQuery.Request, GetAllLogsQuery.Response>
    {
        private readonly IRepository _repository;

        public GetAllLogsHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetAllLogsQuery.Response> Handle(GetAllLogsQuery.Request request, CancellationToken cancellationToken)
        {
            var logs = await _repository.Query<Domain.Entities.Logs>()
                .OrderByDescending(l=>l.DateCreated)
                .Select(GetAllLogsQuery.Response.LogDto.Selector())
                .ToListAsync(cancellationToken);

            return new GetAllLogsQuery.Response
            {
                Logs = logs.Skip(request.PageSize*(request.PageIndex-1)).Take(request.PageSize).ToList()
            };
        }
    }
}