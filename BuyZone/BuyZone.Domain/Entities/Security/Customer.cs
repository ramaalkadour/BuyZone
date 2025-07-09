using BuyZone.Domain.BaseEntity;
using BuyZone.Domain.BaseUser;

namespace BuyZone.Domain.Entities.Security;

public class Customer:User,IBaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }

    public Customer(string firstName, string lastName,string email,string phoneNumber, string address)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}