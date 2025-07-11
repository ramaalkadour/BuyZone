using BuyZone.WAF.Application.BlackIP.Commands.Add;
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
    public async Task<IActionResult> GetAll([FromQuery] GetAllIpsQuery.Request request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody]AddBlockIPCommand.Request request)
    {
        await _mediator.Send(request);
        return Ok();
    }
    
}