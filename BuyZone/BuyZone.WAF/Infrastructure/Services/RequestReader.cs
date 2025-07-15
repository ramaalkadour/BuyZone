using System.Text;
using Microsoft.AspNetCore.Http;

public class RequestReader
{
    public async Task<string> ReadUserInputsAsync(HttpRequest request)
    {
        var builder = new StringBuilder();

        // Query string
        if (request.Query.Any())
        {
            builder.AppendLine("Query Parameters:");
            foreach (var q in request.Query)
                builder.AppendLine($"{q.Key} = {q.Value}");
        }

        // Form fields
        if (request.HasFormContentType)
        {
            request.EnableBuffering();

            var form = await request.ReadFormAsync();

            if (form.Any())
            {
                builder.AppendLine("Form Fields:");
                foreach (var field in form)
                {
                    // تجاهل الحقول المرتبطة بالملفات
                    if (!form.Files.Any(f => f.Name == field.Key))
                    {
                        builder.AppendLine($"{field.Key} = {field.Value}");
                    }
                }
            }

            request.Body.Position = 0;
        }

        // Raw body (e.g. JSON)
        if (request.ContentLength > 0 && request.Body.CanRead)
        {
            request.EnableBuffering();

            request.Body.Position = 0;

            using var reader = new StreamReader(request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
            var body = await reader.ReadToEndAsync();

            if (!string.IsNullOrWhiteSpace(body))
            {
                builder.AppendLine("Raw Body:");
                builder.AppendLine(body);
            }

            request.Body.Position = 0;
        }

        return builder.ToString();
    }
}
