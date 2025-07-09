using BuyZone.Domain.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace BuyZone.Domain.BaseUser;

public class User:IdentityUser<Guid>,IBaseEntity
{
}