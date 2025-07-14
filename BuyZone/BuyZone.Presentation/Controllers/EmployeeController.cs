using BuyZone.Application.Employee.Commands.Add;
using BuyZone.Application.Employee.Commands.Delete;
using BuyZone.Application.Employee.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuyZone.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("GetAll",Name = "GetAll")]
        public async Task<IActionResult> GetAll([FromQuery]GetAllEmployeesQuery.Request request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("Add",Name = "Add")]
        public async Task<IActionResult> Add(AddEmployeeCommand.Request request)
        {
            var result = await _mediator.Send(request);
            return Ok();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult>Delete(DeleteEmployeeCommand.Request request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}