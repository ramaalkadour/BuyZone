using BuyZone.Application.Category.Queries.GetAll;
using MediatR;

namespace BuyZone.Application.Category.Commands.Update;

public class UpdateCategoryHandler:IRequestHandler<UpdateCategoryCommand.Request,GetAllCategoriesQuery.Response>
{
    public Task<GetAllCategoriesQuery.Response> Handle(UpdateCategoryCommand.Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}