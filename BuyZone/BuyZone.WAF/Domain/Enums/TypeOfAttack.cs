namespace BuyZone.WAF.Domain.Enums;

public enum TypeOfAttack
{
    SqlInjection,
    RateLimiting,
    XSS,
    Protected,
}