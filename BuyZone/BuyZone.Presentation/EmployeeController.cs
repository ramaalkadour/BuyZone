using BuyZone.Application.Employee.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuyZone.Presentation;

public class EmployeeController : Controller
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("GetAll")]
    public async Task<IActionResult>GetAll([FromQuery]GetAllEmployeesQuery.Request request)
    {
        return Ok(await _mediator.Send(request));
    }
}