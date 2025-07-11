using BuyZone.Domain.BaseUser;
using BuyZone.Domain.Entities;
using BuyZone.Domain.Entities.Security;
using BuyZone.WAF.Domain.Entities;
using DefaultNamespace;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Infrastructure.DbContest;

public class BuyZoneDbContext:IdentityDbContext<User,Role,Guid>
{
    public BuyZoneDbContext(DbContextOptions<BuyZoneDbContext> options) : base(options)
    {
        
    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<BlockIP> BlackIPs { get; set; }
    public DbSet<Logs>Logs { get; set; }

}