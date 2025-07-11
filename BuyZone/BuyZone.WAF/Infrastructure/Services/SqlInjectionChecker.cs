using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace BuyZone.WAF.Infrastructure.Services
{
    public class SqlInjectionChecker
    {
        private static readonly IReadOnlyList<Regex> Patterns;

        static SqlInjectionChecker()
        {
            Patterns = new List<Regex>
            {
                new Regex(@"\b(select|insert|update|delete|drop|truncate|exec|union|declare|alter|create|shutdown)\b",
                          RegexOptions.IgnoreCase | RegexOptions.Compiled),
                new Regex(@"(--|#|;|\*/|\*/)",
                          RegexOptions.Singleline | RegexOptions.Compiled),

                new Regex(@"\b(or|and)\b\s+1\s*=\s*1",
                          RegexOptions.IgnoreCase | RegexOptions.Compiled),
                
                new Regex(@"\b(or|and)\b\s+\w+\s*=\s*'.+?'",
                          RegexOptions.IgnoreCase | RegexOptions.Compiled),
                
                new Regex(@"\bunion\b\s+(all\s+)?select\b",
                          RegexOptions.IgnoreCase | RegexOptions.Compiled),
                new Regex(@"\b(waitfor\s+delay|sleep\()\b",
                          RegexOptions.IgnoreCase | RegexOptions.Compiled),
                new Regex(@"\b(load_file|into\s+outfile)\b",
                          RegexOptions.IgnoreCase | RegexOptions.Compiled),

                new Regex(@"%2[0-9A-F]{1}", 
                          RegexOptions.IgnoreCase | RegexOptions.Compiled)
            };
        }

        public bool CheckForSqlInjection(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
            
            var decoded = WebUtility.UrlDecode(input);
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
