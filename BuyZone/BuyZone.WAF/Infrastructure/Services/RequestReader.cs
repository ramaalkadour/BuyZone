using System.Text;
using Microsoft.AspNetCore.Http;

public class RequestReader
{
    public async Task<string> ReadUserInputsAsync(HttpRequest request)
    {
        var builder = new StringBuilder();

        // ✅ Add query string parameters
        if (request.Query.Any())
        {
            foreach (var q in request.Query)
                builder.AppendLine($"{q.Key}={q.Value}");
        }

        // ✅ Add body (if any)
        if (request.ContentLength > 0 && request.Body.CanRead)
        {
            request.EnableBuffering();
            request.Body.Position = 0;

            using var reader = new StreamReader(request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            builder.AppendLine(body);
            request.Body.Position = 0;
        }

        return builder.ToString();
    }
}