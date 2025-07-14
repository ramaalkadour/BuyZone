using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Category.Commands.Delete;

public class DeleteCategoryHandler:IRequestHandler<DeleteCategoryCommand.Request>
{
    private readonly IRepository _repository;

    public DeleteCategoryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteCategoryCommand.Request request, CancellationToken cancellationToken)
    {
        var category = await _repository.Query<Domain.Entities.Category>()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (category == null)
            throw new Exception("Category not found");
        _repository.Delete(category);
        await _repository.SaveChangesAsync();
    }
}