using BuyZone.Application.Category.Queries.GetAll;
using MediatR;

namespace BuyZone.Application.Category.Queries.GetById;

public class GetCategoryByIdQuery
{
    public class Request : IRequest<GetAllCategoriesQuery.Response.CategoryDto>
    {
        public Guid Id { get; set; }
    }
}