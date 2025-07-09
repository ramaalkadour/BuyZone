using BuyZone.Application.Category.Queries.GetAll;
using BuyZone.Domain;
using BuyZone.Domain.Entities;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Category.Commands.Update;

public class
    UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand.Request, GetAllCategoriesQuery.Response.CategoryDto>
{
    private readonly IRepository _repository;

    public UpdateCategoryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetAllCategoriesQuery.Response.CategoryDto> Handle(UpdateCategoryCommand.Request request,
        CancellationToken cancellationToken)
    {

        var category = await _repository.Query<Domain.Entities.Category>()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (category == null)
        {
            return null!;
        }


        category.Name = request.Name;
        category.Description = request.Description;


        await _repository.SaveChangesAsync();
        
        return await _repository.Query<Domain.Entities.Category>()
            .Where(c => c.Id == request.Id)
            .Select(GetAllCategoriesQuery.Response.CategoryDto.Selector())
            .FirstAsync(cancellationToken);
    }
}