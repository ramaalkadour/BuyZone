using BuyZone.Domain.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace BuyZone.Domain.BaseUser;

public class User:IdentityUser<Guid>,IBaseEntity
{
    private readonly List<UserRole> _userRoles = new();
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();
}