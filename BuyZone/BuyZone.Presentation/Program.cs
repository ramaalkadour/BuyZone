using System.Text;
using BuyZone.Application;
using BuyZone.Domain;
using BuyZone.Domain.BaseUser;
using BuyZone.Domain.Entities.Security;
using BuyZone.Infrastructure.Auth;
using BuyZone.Infrastructure.DbContest;
using BuyZone.Infrastructure.Dependency_Injection;
using BuyZone.Infrastructure.Services;
using BuyZone.WAF;
using BuyZone.WAF.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<RequestReader>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .WithOrigins("http://localhost:3000")
            .WithOrigins("https://localhost:7065")// نفس ال Origin اللي جاي منو الطلب
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());   
});


builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type =>
    {
        return type.FullName.Replace("+", ".");
    });
});
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddScoped<IJwtService,JwtService>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<BuyZoneDbContext>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddMediatR(o => o.RegisterServicesFromAssemblies(typeof(Class1).Assembly));
builder.Services.AddMediatR(o => o.RegisterServicesFromAssemblies(typeof(AssemblyReference).Assembly));
builder.Services.AddScoped(typeof(WafLogAttribute));
builder.Services.AddScoped(typeof(SqlInjectionChecker));
builder.Services.AddScoped(typeof(XssInjectionChecker));
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<RateLimiter>(provider =>
{
    var cache = provider.GetRequiredService<IMemoryCache>();
    return new RateLimiter(cache, limit: 10, window: TimeSpan.FromSeconds(20));
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(); // تأكد من وجود هذا

app.MapControllers();
app.UseCors("AllowReactApp");
using(var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BuyZoneDbContext>();
    db.Database.Migrate();
}

app.Run();
public class StartUp
{
}