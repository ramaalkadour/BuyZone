using BuyZone.Application.Category.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuyZone.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : Controller
{
   private readonly IMediator _mediator;

   public CategoryController(IMediator mediator)
   {
      _mediator = mediator;
   }
   [HttpGet("GetAll")]
   public async Task<IActionResult> GetAll(GetAllCategoriesQuery.Request request)
   {
      return Ok(await _mediator.Send(request));
   }
}