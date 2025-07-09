using System.Linq.Expressions;
using BuyZone.Application.Employee.Queries.GetAll;
using MediatR;

namespace BuyZone.Application.Employee.Queries.GetById;

public class GetByIdEmployeeQuery
{
    public class Request:IRequest<Response>
    {
        public Guid Id { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
        public string Role { get; set; }

        public static Expression<Func<DefaultNamespace.Employee, GetAllEmployeesQuery.Response.EmployeeRes>> Selector() => e => new()
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email??"",
        };
    }
    }
