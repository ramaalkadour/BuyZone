using Microsoft.EntityFrameworkCore;

namespace BuyZone.Infrastructure.DbContest;

public class BuyZoneDbContext:DbContext
{
    public BuyZoneDbContext(DbContextOptions<BuyZoneDbContext> options) : base(options) { }

}