using MediatR;
using Microsoft.AspNetCore.Mvc;
using BuyZone.Application.Auth;
using BuyZone.Application.Auth.commands.Login;

namespace BuyZone.Presentation.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase 
{
    private readonly IMediator _mediator;

   
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand.Request request)
    {
        
        var response = await _mediator.Send(request);

        
        return Ok(response);
    }
}