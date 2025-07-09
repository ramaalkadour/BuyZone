using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Customer.Commands.Update;

public class UpdateCustomerHandler:IRequestHandler<UpdateCustomerCommand.Request, UpdateCustomerCommand.Response>
{
    private readonly IRepository _repository;

    public UpdateCustomerHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateCustomerCommand.Response> Handle(UpdateCustomerCommand.Request request,  CancellationToken cancellationToken)
    {
        var customer = await _repository.Query<Domain.Entities.Security.Customer>()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (customer == null)
        {
            return new UpdateCustomerCommand.Response
            {
                Success = false,
                Message = "العميل غير موجود"
            };
        }
        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;
        customer.Email = request.Email;
        await _repository.SaveChangesAsync();
        return new UpdateCustomerCommand.Response
        {
            Success = true,
            Message = "تم التحديث بنجاح"
        };
    }
}