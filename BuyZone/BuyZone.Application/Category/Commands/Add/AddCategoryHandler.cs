using BuyZone.Application.Category.Queries.GetAll;
using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Category.Commands.Add;

public class AddCategoryHandler:IRequestHandler<AddCategoryCommand.Request,GetAllCategoriesQuery.Response.CategoryDto>
{
    private readonly IRepository _repository;

    public AddCategoryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetAllCategoriesQuery.Response.CategoryDto> Handle(AddCategoryCommand.Request request, CancellationToken cancellationToken)
    {
        var category = new Domain.Entities.Category(request.Name, request.Description);
        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();
        return await _repository.Query<Domain.Entities.Category>()
            .Where(c=>c.Id==category.Id).Select(GetAllCategoriesQuery.Response.CategoryDto.Selector())
            .FirstAsync(cancellationToken);
    }
}