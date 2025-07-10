using BuyZone.Domain;
using BuyZone.Domain.BaseUser;

namespace DefaultNamespace;

public class Employee:User
{
    public int Number { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public EmployeeStatus Status { get; set; }

    public Employee(string firstName, string lastName,string email ,string phoneNumber,EmployeeStatus status)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        UserName = email;
        Status = status;
    }
}