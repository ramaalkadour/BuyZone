using System.Text.Json;
using BuyZone.Domain;
using BuyZone.WAF.Domain.Entities;
using BuyZone.WAF.Domain.Enums;
using BuyZone.WAF.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class WafLogAttribute : Attribute, IAsyncResourceFilter
{
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        var httpContext = context.HttpContext;
        var services = httpContext.RequestServices;

        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var path = httpContext.Request.Path;

        var rateLimiter = services.GetRequiredService<RateLimiter>();
        var repository = services.GetRequiredService<IRepository>();
        var checker = services.GetRequiredService<SqlInjectionChecker>();
        var checkerXss = services.GetRequiredService<XssInjectionChecker>();
        var requestReader = services.GetRequiredService<RequestReader>();

        // ❌ Blocked IP
        var blockIp = await repository.TrackingQuery<BlockIP>()
            .FirstOrDefaultAsync(i => i.IpAddress == ip);
        if (blockIp is not null)
        {
            blockIp.LastSeen = DateTime.UtcNow;
            blockIp.NumberOfRequests++;
            repository.Update(blockIp);
            await repository.SaveChangesAsync();
        }
        if (blockIp is not null && blockIp.Status == IpStatus.Inactive)
        {
            blockIp.BlockedCount++;
            repository.Update(blockIp);
            await repository.SaveChangesAsync();
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            await httpContext.Response.WriteAsync("Ip Address Is blocked");
            return;
        }
        if (blockIp is null)
        {
            blockIp = new BlockIP(ip, IpStatus.Active);
            blockIp.FirstSeen=DateTime.UtcNow;
            blockIp.NumberOfRequests = 1;
            await repository.AddAsync(blockIp);
            await repository.SaveChangesAsync();
        }
        

        // ❌ Rate limiting
        if (rateLimiter.IsLimited(ip, out var retryAfter))
        {
            blockIp.BlockedCount++;
            repository.Update(blockIp);
            await repository.SaveChangesAsync();
            var blockedLog = new Logs(ip, $"Rate limit exceeded on {path}", path)
            {
                Status = "Blocked",
                TypeOfAttack = TypeOfAttack.RateLimiting
            };

            await repository.AddAsync(blockedLog);
            await repository.SaveChangesAsync();

            httpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            httpContext.Response.Headers["Retry-After"] = retryAfter.TotalSeconds.ToString("F0");

            var response = new
            {
                success = false,
                message = "Too Many Requests. Try again later.",
                retryAfter = retryAfter.TotalSeconds
            };

            var json = JsonSerializer.Serialize(response);
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(json);
            return;
        }

        // 📥 Read full input before model binding
        var fullInput = await requestReader.ReadUserInputsAsync(httpContext.Request);

        // 🧪 Check for SQL injection
        var log = new Logs(ip, fullInput, path);
        if (checker.CheckForSqlInjection(fullInput))
        {
            log.Status = "Blocked";
            log.TypeOfAttack = TypeOfAttack.SqlInjection;
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new
            {
                success = false,
                message = "Invalid request. Possible SQL Injection detected."
            };

            var json = JsonSerializer.Serialize(response);
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(json);
            return;

        }
        if (checkerXss.CheckForXss(fullInput))
        {
            log.Status = "Blocked";
            log.TypeOfAttack = TypeOfAttack.XSS;
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new
            {
                success = false,
                message = "Invalid request. Possible XSS Injection detected."
            };

            var json = JsonSerializer.Serialize(response);
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(json);
            return;

        }
        else
        {
            log.Status = "Accepted";
            log.TypeOfAttack = TypeOfAttack.Protected;
        }

        // 📝 Save log
        await repository.AddAsync(log);
        await repository.SaveChangesAsync();

        // ✅ Continue pipeline
        await next();
    }
}
