using MediatR;

namespace BuyZone.Application.Employee.Commands.Update;

public class UpdateEmployeeCommand
{
    public class Request : IRequest<Response>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
    }

    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}