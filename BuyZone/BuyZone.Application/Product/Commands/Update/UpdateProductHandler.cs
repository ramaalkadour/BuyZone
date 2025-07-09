using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Product.Commands.Update;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand.Request, UpdateProductCommand.Response>
{
    private readonly IRepository _repository;

    public UpdateProductHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateProductCommand.Response> Handle(UpdateProductCommand.Request request, CancellationToken cancellationToken)
    {
        var product = await _repository.Query<Domain.Entities.Product>()
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
        {
            return new UpdateProductCommand.Response
            {
                Success = false,
                Message = "المنتج غير موجود"
            };
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.ImageUrl = request.ImageUrl;
        product.Price = request.Price;
        product.CategoryId = request.CategoryId;

        await _repository.SaveChangesAsync();

        return new UpdateProductCommand.Response
        {
            Success = true,
            Message = "تم تحديث المنتج بنجاح"
        };
    }
}