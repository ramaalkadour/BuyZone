using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Employee.Commands.Delete;

public class DeleteEmployeeHandler:IRequestHandler<DeleteEmployeeCommand.Request>
{
    private readonly IRepository _repository;

    public DeleteEmployeeHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteEmployeeCommand.Request request, CancellationToken cancellationToken)
    {
        var employee=await _repository.TrackingQuery<DefaultNamespace.Employee>()
            .FirstOrDefaultAsync(e=>e.Id==request.Id, cancellationToken);
        if(employee == null)
            throw new Exception("Employee not found");
        _repository.Delete(employee);
        await _repository.SaveChangesAsync();
    }
}