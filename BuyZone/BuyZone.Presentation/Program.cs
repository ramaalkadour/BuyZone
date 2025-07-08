using BuyZone.Application;
using BuyZone.Domain.BaseUser;
using BuyZone.Domain.Entities.Security;
using BuyZone.Infrastructure.DbContest;
using BuyZone.Infrastructure.Dependency_Injection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type =>
    {
        return type.FullName.Replace("+", ".");
    });
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<BuyZoneDbContext>();
builder.Services.AddControllers();
builder.Services.AddMediatR(o => o.RegisterServicesFromAssemblies(typeof(Class1).Assembly));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
public class StartUp
{
}