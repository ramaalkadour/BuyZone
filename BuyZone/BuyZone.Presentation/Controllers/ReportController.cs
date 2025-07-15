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

    [HttpGet("GetReport")]
    [WafLog]
    public async Task<IActionResult> GetCustomersWithOrders([FromQuery]GetReportQuery.Request request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
