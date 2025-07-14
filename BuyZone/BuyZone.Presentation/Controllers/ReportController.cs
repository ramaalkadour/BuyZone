using BuyZone.Application.Report.Queries.GetReportQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuyZone.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("customer-orders")]
    public async Task<IActionResult> GetCustomersWithOrders()
    {
        var result = await _mediator.Send(new GetReportQuery());
        return Ok(result);
    }
}
