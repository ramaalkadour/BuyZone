using BuyZone.Application.Category.Commands;
using BuyZone.Application.Category.Commands.Add;
using BuyZone.Application.Category.Commands.Delete;
using BuyZone.Application.Category.Queries.GetAll;
using BuyZone.Domain;
using BuyZone.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuyZone.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CategoryController : Controller
{
   private readonly IMediator _mediator;
   private readonly IRepository _repository;
   public CategoryController(IMediator mediator, IRepository repository)
   {
      _mediator = mediator;
      _repository = repository;
   }
   [HttpGet("GetAll")]
   [WafLog]
   public async Task<IActionResult> GetAll([FromQuery]GetAllCategoriesQuery.Request request)
   {
      return Ok(await _mediator.Send(request));
   }

   [HttpPost("Add")]
   [WafLog]
   public async Task<IActionResult> Add(AddCategoryCommand.Request request)
   {
      return Ok(await _mediator.Send(request));
   }
   [HttpDelete("Delete")]
   [WafLog]
   public async Task<IActionResult>Delete(DeleteCategoryCommand.Request request)
   {
      await _mediator.Send(request);
      return Ok();
   }
}