using BuyZone.Application.Employee.Queries.GetAll;
using BuyZone.Domain;
using MediatR;

namespace BuyZone.Application.Employee.Commands.Add;

public class AddEmployeeCommand
{
    public class Request:IRequest<GetAllEmployeesQuery.Response.EmployeeRes>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public EmployeeStatus Status { get; set; }
        public Guid RoleId { get; set; }
    }
}