using MediatR;

namespace BuyZone.Application.Category.Commands.Delete;

public class DeleteCategoryCommand
{
    public class Request:IRequest
    {
        public Guid Id { get; set; }
    }
    
}