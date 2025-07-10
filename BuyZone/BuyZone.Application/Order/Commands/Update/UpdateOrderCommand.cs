using MediatR;

namespace BuyZone.Application.Order.Commands.Update;

public class UpdateOrderCommand
{
    public class Request:IRequest<Response>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public Domain.Entities.Product Product { get; set; }
        public string ProductName { get; set; }

        public double Price { get; set; } 
    }

    public class Response
    {
       public  bool Success { get; set; }
       public string Message { get; set; }
    }
    
    
}