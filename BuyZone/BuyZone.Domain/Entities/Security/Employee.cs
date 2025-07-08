using BuyZone.Domain.BaseUser;

namespace DefaultNamespace;

public class Employee:User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Employee(string firstName, string lastName,string email ,string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}