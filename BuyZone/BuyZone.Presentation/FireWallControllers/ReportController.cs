
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuyZone.Presentation.FireWallControllers;
[ApiController]
[Route("api/[controller]")]
public class ReportController : Controller
{
    private readonly IMediator _mediator;

    public ReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery]GetStaticByTypeOfAttackQuery.Request request)
    {
        return Ok(await _mediator.Send(request));
    }
}