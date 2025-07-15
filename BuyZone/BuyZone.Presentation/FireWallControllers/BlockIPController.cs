using System.Data;
using BuyZone.WAF.Application.BlackIP.Commands.Add;
using BuyZone.WAF.Application.BlackIP.Commands.UpdateStatus;
using BuyZone.WAF.Application.BlackIP.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuyZone.Presentation.FireWallControllers;

[ApiController]
[Route("api/[controller]")]
public class BlockIPController : Controller
{
    private readonly IMediator _mediator;

    public BlockIPController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAll")]
    [WafLog]
    public async Task<IActionResult> GetAll([FromQuery] GetAllIpsQuery.Request request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost("Add")]
    [WafLog]
    public async Task<IActionResult> Add([FromBody]AddBlockIPCommand.Request request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpPut("UpdateStatus")]
    [WafLog]
    public async Task<IActionResult> UpdateStatus([FromQuery] UpdateStatusCommand.Request request)
    {
        await _mediator.Send(request);
        return Ok();
    }
    
}