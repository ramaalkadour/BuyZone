using BuyZone.Application.Product.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuyZone.Application.Auth.commands.Login;

public class LoginCommand
{
    public class Request : IRequest<Response>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Response
    {
        
        public string Token { get; set; }
        public string UserName { get; set; }
    }
}