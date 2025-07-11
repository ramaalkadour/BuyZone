using System.Text;
using BuyZone.Domain;
using BuyZone.WAF.Domain.Entities;
using BuyZone.WAF.Domain.Enums;
using BuyZone.WAF.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

public class WafLogAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var path = context.HttpContext.Request.Path;

        var rateLimiter = context.HttpContext.RequestServices.GetRequiredService<RateLimiter>();
        var _repository = context.HttpContext.RequestServices.GetRequiredService<IRepository>();
        var checker = context.HttpContext.RequestServices.GetRequiredService<SqlInjectionChecker>();
        var request = context.HttpContext.Request;

        // Check rate limiting
        if (rateLimiter.IsLimited(ip, out var retryAfter))
        {
            var blockedLog = new Logs(ip, request: $"Rate limit exceeded on {path}", path: path)
            {
                Status = "Blocked",
                TypeOfAttack = TypeOfAttack.RateLimiting
            };
            await _repository.AddAsync(blockedLog);
            await _repository.SaveChangesAsync();

            context.HttpContext.Response.StatusCode = 429;
            context.HttpContext.Response.Headers["Retry-After"] = retryAfter.TotalSeconds.ToString("F0");
            await context.HttpContext.Response.WriteAsync("Too Many Requests");
            return;
        }

        // Build full input for logging & SQL injection check
        var stringBuilder = new StringBuilder();

        if (request.Query.Any())
        {
            foreach (var q in request.Query)
                stringBuilder.AppendLine($"{q.Key}={q.Value}");
        }

        foreach (var h in request.Headers)
            stringBuilder.AppendLine($"{h.Key}:{h.Value}");

        request.EnableBuffering();
        if (request.ContentLength > 0 && request.Body.CanRead)
        {
            request.Body.Position = 0;
            using var reader = new StreamReader(request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            stringBuilder.AppendLine(body);
            request.Body.Position = 0;
        }

        var fullInput = stringBuilder.ToString();
        var log = new Logs(ip, request: fullInput, path: path);

        if (checker.CheckForSqlInjection(fullInput))
        {
            log.Status = "Blocked";
            log.TypeOfAttack = TypeOfAttack.SqlInjection;
        }

        await _repository.AddAsync(log);
        await _repository.SaveChangesAsync();

        await next(); // continue request
    }
}
