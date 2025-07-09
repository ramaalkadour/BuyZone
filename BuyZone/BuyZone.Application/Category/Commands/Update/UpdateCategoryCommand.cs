using System.Linq.Expressions;
using BuyZone.Application.Category.Queries.GetAll;
using BuyZone.Application.Category.Queries.GetById;
using MediatR;

namespace BuyZone.Application.Category.Commands.Update;

public class UpdateCategoryCommand
{
    public class Request:IRequest<GetAllCategoriesQuery.Response.CategoryDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}