using BuyZone.Application.Order.Commands.Update;
using BuyZone.Application.Order.Queries.GetAll;
using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Order.Commands.Update;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand.Request, UpdateOrderCommand.Response>
{
    private readonly IRepository _repository;

    public UpdateOrderHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateOrderCommand.Response> Handle(UpdateOrderCommand.Request request, CancellationToken cancellationToken)
    {
        var order = await _repository.Query<Domain.Entities.Order>()
            .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        if (order == null)
        {
            return new UpdateOrderCommand.Response
            {
                Success = false,
                Message = "الطلب غير موجود"
            };
        }

        // تحديث البيانات
        order.CustomerId = request.CustomerId;
        order.ProductId = request.ProductId;
        order.Price = request.Price;
        order.ProductName = request.ProductName;

        await _repository.SaveChangesAsync();

        return new UpdateOrderCommand.Response
        {
            Success = true,
            Message = "تم تحديث الطلب بنجاح"
        };
    }
}