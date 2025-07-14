using BuyZone.Domain.BaseEntity;
using BuyZone.Domain.BaseUser;

namespace BuyZone.Domain.Entities.Security;

public class Customer:User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public CustomerEnum Status { get; set; }
    private readonly List<Order> _orders = new();
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();


    public Customer(string firstName, string lastName,string email,string phoneNumber, string address,CustomerEnum status)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        UserName = email;
        Status = status;
    }
}