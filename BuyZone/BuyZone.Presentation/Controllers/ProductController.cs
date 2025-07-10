using BuyZone.Application.Category.Queries.GetAll;
using BuyZone.Application.Product.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuyZone.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery]GetAllProductsQuery.Request request)
    {
        return Ok(await _mediator.Send(request));
    }
}