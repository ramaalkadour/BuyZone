using System.Linq.Expressions;
using BuyZone.Application.Customer.Queries.GetAll;
using MediatR;

namespace BuyZone.Application.Customer.Queries.GetById;

public class GetByIdCustomerQuery
{
    public class Request : IRequest<Response>
    {
        public Guid Id { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public static Expression<Func<Domain.Entities.Security.Customer, Response>> Selector() => c => new()
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
        };
    }

   
}