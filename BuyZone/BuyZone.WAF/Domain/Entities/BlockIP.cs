using System.Net.NetworkInformation;
using BuyZone.Domain.BaseEntity;
using BuyZone.WAF.Domain.Enums;

namespace BuyZone.WAF.Domain.Entities;

public class BlockIP:IBaseEntity
{
    public Guid Id { get; set; }

    public long NumberOfRequests { get; set; }
    public string IpAddress { get; set; }

    public int SqlInjectionAttempts { get; set; } 
    
    public int BlockedCount { get; set; } 

    public int RateLimitViolations { get; set; } 

    public long SafeRequestCount { get; set; } 

    public DateTime FirstSeen { get; set; }

    public DateTime LastSeen { get; set; }
    public IpStatus Status { get; set; }

    public BlockIP(string ipAddress,IpStatus status)
    {
        IpAddress = ipAddress;
        Status = status;
    }

    public BlockIP(long numberOfRequests, string ipAddress, int sqlInjectionAttempts, int blockedCount, int rateLimitViolations, long safeRequestCount, DateTime firstSeen, DateTime lastSeen, IpStatus status)
    {
        NumberOfRequests = numberOfRequests;
        IpAddress = ipAddress;
        SqlInjectionAttempts = sqlInjectionAttempts;
        BlockedCount = blockedCount;
        RateLimitViolations = rateLimitViolations;
        SafeRequestCount = safeRequestCount;
        FirstSeen = firstSeen;
        LastSeen = lastSeen;
        Status = status;
    }
}