using System.Linq.Expressions;
using BuyZone.Domain;
using MediatR;

namespace BuyZone.Application.Employee.Queries.GetAll;

public class GetAllEmployeesQuery
{
    public class Request:IRequest<Response>
    {
        
    }

    public class Response
    {
        public int Count { get; set; }
        public List<EmployeeRes>Employees { get; set; }

        public class EmployeeRes
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public EmployeeStatus Status { get; set; }

            public static Expression<Func<DefaultNamespace.Employee, EmployeeRes>> Selector() => e => new()
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email??"",
                PhoneNumber = e.PhoneNumber,
                Status = e.Status,
            };
        }
    }
}