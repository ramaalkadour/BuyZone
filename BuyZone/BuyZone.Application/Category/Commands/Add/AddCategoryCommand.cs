using BuyZone.Application.Category.Queries.GetAll;
using MediatR;

namespace BuyZone.Application.Category.Commands.Add;

public class AddCategoryCommand
{
    public class Request:IRequest<GetAllCategoriesQuery.Response.CategoryDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}