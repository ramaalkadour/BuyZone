using BuyZone.Application.Customer.Commands.Add;
using BuyZone.Application.Customer.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuyZone.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll", Name = "GetAllCustomers")]
        public async Task<IActionResult> GetAll(GetAllCustomerQuery.Request request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("Add", Name = "AddCustomer")]
        public async Task<IActionResult> Add(AddCustomerCommand.Request request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}