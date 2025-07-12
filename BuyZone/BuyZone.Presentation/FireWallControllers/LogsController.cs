using BuyZone.WAF.Application.Logs.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuyZone.Presentation.FireWallControllers;

[ApiController]
[Route("api/[controller]")]
public class LogsController:Controller
{
    private readonly IMediator _mediator;

    public LogsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("GetAll")]
    [WafLog]
    public async Task<IActionResult> GetAll([FromQuery]GetAllLogsQuery.Request request)
    {
        return Ok(await _mediator.Send(request));
    }
}