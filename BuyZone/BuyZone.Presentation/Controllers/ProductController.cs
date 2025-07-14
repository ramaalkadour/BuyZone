using BuyZone.Application.Category.Queries.GetAll;
using BuyZone.Application.Product.Commands.AddOrUpdate;
using BuyZone.Application.Product.Commands.Delete;
using BuyZone.Application.Product.Queries.GetAll;
using BuyZone.Application.Product.Queries.GetById;
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
    [WafLog]
    public async Task<IActionResult> GetAll([FromQuery]GetAllProductsQuery.Request request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost("Add")]
    [WafLog]
    public async Task<IActionResult> Add([FromForm] AddOrUpdateProductCommand.Request request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete("Delete")]
    [WafLog]
    public async Task<IActionResult> Delete([FromQuery] DeleteProductCommand.Request request)
    {
        await _mediator.Send(request);
        return Ok();
    }
    [HttpGet("GetById")]
    [WafLog]
    public async Task<IActionResult> GetById([FromQuery] GetByIdProductQuery.Request request)
    {
        return Ok(await _mediator.Send(request));
    }
}