using BuyZone.Domain.BaseUser;
using BuyZone.Domain.Entities;
using BuyZone.Domain.Entities.Security;
using DefaultNamespace;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.Infrastructure.DbContest;

public class BuyZoneDbContext:IdentityDbContext<User,Role,Guid>
{
    public BuyZoneDbContext(DbContextOptions<BuyZoneDbContext> options) : base(options) { }
    public DbSet<Employee> Employees ;
    public DbSet<Category> Categories;
    

}