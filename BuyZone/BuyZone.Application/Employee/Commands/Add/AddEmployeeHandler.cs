using BuyZone.Application.Employee.Queries.GetAll;
using BuyZone.Domain;
using BuyZone.Domain.BaseUser;
using BuyZone.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Employee.Commands.Add;

public class AddEmployeeHandler:IRequestHandler<AddEmployeeCommand.Request,GetAllEmployeesQuery.Response.EmployeeRes>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Domain.Entities.Security.Role> _roleManager;
    private readonly IRepository _repository;
    public AddEmployeeHandler(UserManager<User> userManager, RoleManager<Domain.Entities.Security.Role> roleManager, IRepository repository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _repository = repository;
    }

    public async Task<GetAllEmployeesQuery.Response.EmployeeRes> Handle(AddEmployeeCommand.Request request, CancellationToken cancellationToken)
    {
        var employee = new DefaultNamespace.Employee(request.FirstName,request.LastName,request.Email,request.PhoneNumber,request.Status);
        var result=await _userManager.CreateAsync(employee, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new Exception($"فشل إنشاء الموظف: {errors}");
        }

        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role is not null)
            await _userManager.AddToRoleAsync(employee, role.Name??"");
        return await _repository.Query<DefaultNamespace.Employee>()
            .Where(e => e.Id == employee.Id).Select(GetAllEmployeesQuery.Response.EmployeeRes.Selector())
            .FirstAsync(cancellationToken);
    }
}