using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Product.Commands.Delete;

public class DeleteProductHandler:IRequestHandler<DeleteProductCommand.Request>
{
    private readonly IRepository _repository;

    public DeleteProductHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteProductCommand.Request request, CancellationToken cancellationToken)
    {
        var product = await _repository.TrackingQuery<Domain.Entities.Product>()
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);
        if (product == null)
            throw new Exception("Product not found");
        _repository.Delete(product);
         await _repository.SaveChangesAsync();
        
    }
}