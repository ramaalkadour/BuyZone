using BuyZone.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BuyZone.Application.Report.Queries.GetReportQuery;
using BuyZone.Domain;
using BuyZone.Domain.Entities;
using BuyZone.Domain.Entities.Security;

public class GetReportQueryHandler : IRequestHandler<GetReportQuery.Request, GetReportQuery.Response>
{
    private readonly IRepository _repository;

    public GetReportQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<GetReportQuery.Response> Handle(GetReportQuery.Request request, CancellationToken cancellationToken)
    {
        return new GetReportQuery.Response
        {
            CustomerCount = await _repository.Query<Customer>().CountAsync(cancellationToken),
            OrderCount = await _repository.Query<Order>().CountAsync(cancellationToken)
        };
    }
}