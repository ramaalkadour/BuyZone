using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Application.Category.Queries.GetAll;

public class GetAllCategoriesHandler:IRequestHandler<GetAllCategoriesQuery.Request,GetAllCategoriesQuery.Response>
{
    private readonly IRepository _repository;

    public GetAllCategoriesHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetAllCategoriesQuery.Response> Handle(GetAllCategoriesQuery.Request request, CancellationToken cancellationToken)
    {
        var categories = await _repository.Query<Domain.Entities.Category>()
            .Select(GetAllCategoriesQuery.Response.CategoryDto.Selector())
            .ToListAsync(cancellationToken);
        return new GetAllCategoriesQuery.Response()
        {
            Count = categories.Count,
            Categories = categories
        };
    }
}