using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace BuyZone.WAF.Infrastructure.Services
{
    public class XssInjectionChecker
    {
        private static readonly IReadOnlyList<Regex> Patterns;

        static XssInjectionChecker()
        {
            Patterns = new List<Regex>
            {
                // <script>, <img src=..., onerror=...>, etc.
                new Regex(@"<\s*script[^>]*>.*?<\s*/\s*script\s*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled),
                new Regex(@"<\s*[^>]*on\w+\s*=\s*[""']?.*?[""']?", RegexOptions.IgnoreCase | RegexOptions.Compiled), // Events like onclick, onerror
                new Regex(@"<\s*iframe[^>]*>.*?<\s*/\s*iframe\s*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled),
                new Regex(@"<\s*img[^>]*src\s*=\s*[""']?javascript:.*?[""']?", RegexOptions.IgnoreCase | RegexOptions.Compiled),
                new Regex(@"javascript:", RegexOptions.IgnoreCase | RegexOptions.Compiled),
                new Regex(@"vbscript:", RegexOptions.IgnoreCase | RegexOptions.Compiled),
                new Regex(@"data:text/html", RegexOptions.IgnoreCase | RegexOptions.Compiled),
                new Regex(@"<\s*object[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled),
                new Regex(@"<\s*embed[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled),
                new Regex(@"<\s*form[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled),
                new Regex(@"<\s*svg[^>]*>.*?<\s*/\s*svg\s*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled),
                new Regex(@"<\s*meta[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled)
            };
        }

        public bool CheckForXss(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // فك الترميز إذا كان input مشفر
            var decoded = WebUtility.HtmlDecode(input);
            var normalized = Regex.Replace(decoded, @"\s+", " ").Trim();

            foreach (var pattern in Patterns)
            {
                if (pattern.IsMatch(normalized))
                    return true;
            }

            return false;
        }
    }
}
