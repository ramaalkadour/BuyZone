using BuyZone.WAF.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuyZone.Domain;
using BuyZone.WAF.Domain.Entities;
using BuyZone.WAF.Domain.Enums;
using Microsoft.AspNetCore.Http;

public class WafLogAttribute : Attribute,IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var _repository = context.HttpContext.RequestServices.GetRequiredService<IRepository>();
        var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        var requestPath = context.HttpContext.Request.Path;
        var request = context.HttpContext.Request;
        var checker = context.HttpContext.RequestServices.GetRequiredService<SqlInjectionChecker>();
        
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
        var log = new Logs(ip, request: fullInput,
            path: requestPath);
        if (checker.CheckForSqlInjection(fullInput))
        {
            log.Status = "Blocked";
            log.TypeOfAttack = TypeOfAttack.SqlInjection;
        }
        await _repository.AddAsync(log);
        await _repository.SaveChangesAsync();
        await next();
    }
}
