using BuyZone.Application.Product.Queries.GetAll;
using BuyZone.Domain;
using BuyZone.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Product.Commands.AddOrUpdate;

public class AddProductHandler : IRequestHandler<AddOrUpdateProductCommand.Request, AddOrUpdateProductCommand.Response>
{
    private readonly IRepository _repository;

    public AddProductHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<AddOrUpdateProductCommand.Response> Handle(AddOrUpdateProductCommand.Request request, CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            Price = request.Price,
            CategoryId = request.CategoryId
        };

        await _repository.AddAsync(product, cancellationToken);
        await _repository.SaveChangesAsync();

        
        return new AddOrUpdateProductCommand.Response
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            CategoryId = product.CategoryId
        };
    }
}