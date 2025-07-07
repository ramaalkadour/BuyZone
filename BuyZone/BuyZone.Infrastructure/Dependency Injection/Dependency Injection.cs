using BuyZone.Domain;
using BuyZone.Domain.BaseUser;
using BuyZone.Domain.Entities.Security;
using BuyZone.Infrastructure.DbContest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;


namespace BuyZone.Infrastructure.Dependency_Injection;

public static class Dependency_Injection
{
    public static IServiceCollection AddInfrastructure( this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BuyZoneDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped(typeof(IRepository), typeof(Repository));
        return services;
    }
}