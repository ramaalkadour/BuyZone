using BuyZone.Domain.Entities.Security;
using Microsoft.AspNetCore.Identity;

namespace BuyZone.Domain.BaseUser;

public class UserRole:IdentityUserRole<Guid>
{
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}