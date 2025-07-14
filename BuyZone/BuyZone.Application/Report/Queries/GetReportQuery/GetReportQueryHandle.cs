using BuyZone.Application.Interfaces;
using BuyZone.Application.Report.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BuyZone.Application.Report.Queries.GetReportQuery;
using BuyZone.Domain;

public class GetReportQueryHandler : IRequestHandler<GetReportQuery, List<CustomerWithOrderCountDto>>
{
    private readonly IRepository _repository;

    public GetReportQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<CustomerWithOrderCountDto>> Handle(GetReportQuery request, CancellationToken cancellationToken)
    {
        var customers = await _repository.Customers
            .Select(c => new CustomerWithOrderCountDto
            {
                CustomerId = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                OrdersCount = _repository.Orders.Count(o => o.CustomerId == c.Id)
            })
            .ToListAsync(cancellationToken);

        return customers;
    }
}