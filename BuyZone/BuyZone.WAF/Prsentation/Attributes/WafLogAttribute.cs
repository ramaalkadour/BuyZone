using BuyZone.Domain;
using BuyZone.WAF.Domain.Entities;
using BuyZone.WAF.Domain.Enums;
using BuyZone.WAF.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class WafLogAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;
        var services = httpContext.RequestServices;

        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var path = httpContext.Request.Path;

        var rateLimiter = services.GetRequiredService<RateLimiter>();
        var repository = services.GetRequiredService<IRepository>();
        var checker = services.GetRequiredService<SqlInjectionChecker>();
        var requestReader = services.GetRequiredService<RequestReader>();
        var blockIp=await repository.Query<BlockIP>()
            .FirstOrDefaultAsync(i=>i.IpAddress == ip);
        if(blockIp is not null&&blockIp.Status==IpStatus.Inactive)
        {
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            await httpContext.Response.WriteAsync("Ip Address Is blocked");
            return;
        }
        // 🛑 Rate limiting
        if (rateLimiter.IsLimited(ip, out var retryAfter))
        {
            var blockedLog = new Logs(ip, $"Rate limit exceeded on {path}", path)
            {
                Status = "Blocked",
                TypeOfAttack = TypeOfAttack.RateLimiting
            };

            await repository.AddAsync(blockedLog);
            await repository.SaveChangesAsync();

            httpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            httpContext.Response.Headers["Retry-After"] = retryAfter.TotalSeconds.ToString("F0");
            await httpContext.Response.WriteAsync("Too Many Requests");
            return;
        }

        // 📥 Read full input
        var fullInput = await requestReader.ReadUserInputsAsync(httpContext.Request);

        // 🧪 Check for SQL injection
        var log = new Logs(ip, fullInput, path);
        if (checker.CheckForSqlInjection(fullInput))
        {
            log.Status = "Blocked";
            log.TypeOfAttack = TypeOfAttack.SqlInjection;
        }
        else
        {
            log.Status = "Accepted";
            log.TypeOfAttack = TypeOfAttack.Protected;
        }

        // 📝 Save log
        await repository.AddAsync(log);
        await repository.SaveChangesAsync();

        // ✅ Continue
        await next();
    }
}