using MediatR;

namespace BuyZone.Application.Product.Commands.Delete;

public class DeleteProductCommand
{
    public class Request:IRequest
    {
        public Guid Id { get; set; }
    }
}