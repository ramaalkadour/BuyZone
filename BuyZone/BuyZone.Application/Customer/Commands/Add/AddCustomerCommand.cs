using BuyZone.Application.Customer.Queries.GetAll;
using BuyZone.Domain.Entities.Security;
using MediatR;

namespace BuyZone.Application.Customer.Commands.Add;

public class AddCustomerCommand
{
    public class Request:IRequest<GetAllCustomerQuery.Response.CustomerRes>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public CustomerEnum Status { get; set; }
    }
}