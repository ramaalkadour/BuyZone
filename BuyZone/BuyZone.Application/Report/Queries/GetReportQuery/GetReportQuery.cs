using MediatR;

namespace BuyZone.Application.Report.Queries.GetReportQuery;

using MediatR;
using System.Collections.Generic;

public class GetReportQuery : IRequest<List<CustomerWithOrderCountDto>>
{
}
public class CustomerWithOrderCountDto
{
    public int CustomerId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public int OrdersCount { get; set; }
}
