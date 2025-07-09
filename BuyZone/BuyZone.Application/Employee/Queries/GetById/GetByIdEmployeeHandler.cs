using BuyZone.Application.Customer.Queries.GetById;
using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Employee.Queries.GetById;

public class GetByIdEmployeeHandler:IRequestHandler<GetByIdEmployeeQuery.Request,GetByIdEmployeeQuery.Response>
{
    private readonly IRepository _repository;

    public GetByIdEmployeeHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetByIdEmployeeQuery.Response> Handle(GetByIdEmployeeQuery.Request request, CancellationToken cancellationToken)
    {
        
        var employee =await _repository.Query<DefaultNamespace.Employee>()
            .Where(c => c.Id == request.Id)
            .Select(GetByIdEmployeeQuery.Response.Selector())
            .FirstAsync(cancellationToken);
        return employee;
    }
}