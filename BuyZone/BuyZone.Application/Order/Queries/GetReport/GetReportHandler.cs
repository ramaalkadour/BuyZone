using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Order.Queries.GetReport
{
    public class GetReportHandler : IRequestHandler<GetReportQuery.Request, GetReportQuery.Response>
    {
        private readonly IRepository _repository;

        public GetReportHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetReportQuery.Response> Handle(GetReportQuery.Request request, CancellationToken cancellationToken)
        {
            var query = _repository.Query<Domain.Entities.Order>();

            var groupedData = await query
                .GroupBy(o => o.DateCreated)
                .Select(g => new GetReportQuery.Response.DayStatistics
                {
                    Date = g.Key,
                    NumberOfAttempts = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync(cancellationToken);

            return new GetReportQuery.Response
            {
                Days = groupedData
            };
        }
    }
}