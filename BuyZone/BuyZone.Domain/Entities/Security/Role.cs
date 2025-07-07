using BuyZone.Domain.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace BuyZone.Domain.Entities.Security;

public class Role:IdentityRole<Guid>,IBaseEntity
{
    
}