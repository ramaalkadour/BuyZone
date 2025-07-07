using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Employee.Queries.GetAll;

public class GetAllEmployeesHandler:IRequestHandler<GetAllEmployeesQuery.Request,GetAllEmployeesQuery.Response>
{
    private readonly IRepository _repository;

    public GetAllEmployeesHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetAllEmployeesQuery.Response> Handle(GetAllEmployeesQuery.Request request, CancellationToken cancellationToken)
    {
        var employees = await _repository.Query<DefaultNamespace.Employee>()
            .Select(GetAllEmployeesQuery.Response.EmployeeRes.Selector()).ToListAsync(cancellationToken);
        return new GetAllEmployeesQuery.Response()
        {
            Count = employees.Count,
            Employees = employees
        };
    }
}