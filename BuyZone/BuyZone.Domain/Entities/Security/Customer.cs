using BuyZone.Domain.BaseEntity;
using BuyZone.Domain.BaseUser;

namespace BuyZone.Domain.Entities.Security;

public class Customer:User,IBaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
}