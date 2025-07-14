using MediatR;

namespace BuyZone.Application.Employee.Commands.Delete;

public class DeleteEmployeeCommand
{
    public class Request:IRequest
    {
        public Guid Id { get; set; }
    }
}