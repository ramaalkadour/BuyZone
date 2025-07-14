using System.Linq.Expressions;
using BuyZone.Domain.Entities.Security;
using MediatR;

namespace BuyZone.Application.Customer.Queries.GetAll;

public class GetAllCustomerQuery
{
    public class Request:IRequest<Response>
    {
        
    }

    public class Response
    {
        public int Count { get; set; }
        public List<CustomerRes> Customers { get; set; }
        public class CustomerRes
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public int NumberOfOrders { get; set; }
            public CustomerEnum ?Status { get; set; }

            public static Expression<Func<Domain.Entities.Security.Customer, CustomerRes>> Selector() => c => new()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email??"",
                PhoneNumber = c.PhoneNumber??"",
                NumberOfOrders = c.Orders.Count,
                Status = c.Status,
            };
        }
    }
}