using BuyZone.Application.Role.Commands;
using BuyZone.Application.Role.Commands.Delete;
using BuyZone.Application.Role.Commands.Update;
using BuyZone.Application.Role.Queries.GetAll;
using BuyZone.Application.Role.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuyZone.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllRolesQuery.Request());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetRoleByIdQuery.Request { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddRoleCommand.Request request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateRoleCommand.Request request)
        {
            if (id != request.Id)
                return BadRequest("Id mismatch.");
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteRoleCommand.Request { Id = id });
            return Ok(result);
        }
    }
}