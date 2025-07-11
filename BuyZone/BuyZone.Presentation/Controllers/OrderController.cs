using BuyZone.Application.Order.Commands.Add;
using BuyZone.Application.Order.Commands.Update;
using BuyZone.Application.Order.Queries.GetAll;
using BuyZone.Application.Order.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuyZone.Presentation.Controllers;

public class OrderController : Controller
{
   private readonly IMediator _mediator;

   public OrderController(IMediator mediator)
   {
      _mediator = mediator;
   }

   [HttpGet("GetAll")]
   public async Task<IActionResult> GetAll([FromQuery]GetAllOrdersQuery.Request request)
   {
      return Ok(await _mediator.Send(request));
   }

   [HttpGet("GetById")]
   public async Task<IActionResult> GetById([FromQuery] GetByIdOrderQuery.Request request)
   {
      return Ok(await _mediator.Send(request));
   }

   [HttpPost("Add")]
   public async Task<IActionResult> AddOrder([FromQuery] AddOrderCommand.Request request)
   {
      return Ok(await _mediator.Send(request));
   }

   [HttpPost("Update")]
   public async Task<IActionResult> UpdateOrder([FromQuery] UpdateOrderCommand.Request request)
   {
      return Ok(await _mediator.Send(request));
   }
}