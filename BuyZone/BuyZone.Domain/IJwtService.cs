using BuyZone.Domain.BaseUser;

namespace BuyZone.Domain;

public interface IJwtService
{
    string GenerateToken(User user);
}