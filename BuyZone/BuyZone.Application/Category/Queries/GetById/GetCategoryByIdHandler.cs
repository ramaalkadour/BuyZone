using BuyZone.Application.Category.Queries.GetAll;
using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Category.Queries.GetById;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery.Request, GetAllCategoriesQuery.Response.CategoryDto>
{
    private readonly IRepository _repository;

    public GetCategoryByIdHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetAllCategoriesQuery.Response.CategoryDto> Handle(GetCategoryByIdQuery.Request request, CancellationToken cancellationToken)
    {
        var category = await _repository.Query<Domain.Entities.Category>()
            .Where(c => c.Id == request.Id)
            .Select(GetAllCategoriesQuery.Response.CategoryDto.Selector())
            .FirstOrDefaultAsync(cancellationToken);

        if (category == null)
            throw new Exception("Category not found");

        return category;
    }
}