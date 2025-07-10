using BuyZone.Domain;
using BuyZone.Domain.BaseUser;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuyZone.Application.Auth.commands.Login;

public class LoginHandler:IRequestHandler<LoginCommand.Request, LoginCommand.Response>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _ijwtService;

    public LoginHandler(UserManager<User> userManager, IJwtService ijwtService)
    {
        _userManager = userManager;
        _ijwtService = ijwtService;
    }
    public  async Task<LoginCommand.Response> Handle(LoginCommand.Request request, CancellationToken cancellationToken)
    {
       var user=await _userManager.FindByEmailAsync(request.Email);
       if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
           throw new Exception("Invalid credentials");
       var token = _ijwtService.GenerateToken(user);
       return new LoginCommand.Response
       {
           Token = token,
           UserName = user.UserName
       };

    }
}