using BuyZone.Domain.BaseEntity;
using BuyZone.WAF.Domain.Enums;

namespace BuyZone.WAF.Domain.Entities;

public class Logs:IBaseEntity
{
    public Guid Id { get; set; }
    public string IpAddress { get; set; }
    public TypeOfAttack? TypeOfAttack { get; set; }
    public string Status { get; set; }
    public string Path { get; set; }
    public string Request { get; set; }
    public DateTime DateCreated { get; set; } 

    public Logs(string ipAddress, string request,string path)
    {
        IpAddress = ipAddress;
        Request = request;
        Path = path;
        DateCreated=DateTime.UtcNow;
    }

    public Logs(string ipAddress, TypeOfAttack? typeOfAttack, string status, string request, DateTime dateCreated)
    {
        IpAddress = ipAddress;
        TypeOfAttack = typeOfAttack;
        Status = status;
        Request = request;
        DateCreated = dateCreated;
    }
}