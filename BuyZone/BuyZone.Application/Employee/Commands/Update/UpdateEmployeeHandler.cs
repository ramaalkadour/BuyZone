using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Employee.Commands.Update;

public class UpdateEmployeeHandler:IRequestHandler<UpdateEmployeeCommand.Request, UpdateEmployeeCommand.Response>   
{
    private readonly IRepository _repository;
    public UpdateEmployeeHandler(IRepository repository)
        {
        _repository = repository;
        }
    

    public async Task<UpdateEmployeeCommand.Response> Handle(UpdateEmployeeCommand.Request request, CancellationToken cancellationToken)
    {
        var employee =await _repository.Query<DefaultNamespace.Employee>()
            .FirstOrDefaultAsync(e=>e.Id==request.Id, cancellationToken);
        if(employee == null)
            return new UpdateEmployeeCommand.Response
            {
                Success = false,
                Message = "الموظف غير موجود"
            };
        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Email = request.Email;
        await _repository.SaveChangesAsync();
        return new UpdateEmployeeCommand.Response
        {
            Success = true,
            Message = "تم التحديث بنجاح"
        };
        
    }
}