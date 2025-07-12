using Microsoft.Extensions.Caching.Memory;

public class RateLimiter
{
    private readonly IMemoryCache _cache;
    private readonly int _limit;
    private readonly TimeSpan _window;

    public RateLimiter(IMemoryCache cache, int limit, TimeSpan window)
    {
        _cache = cache;
        _limit = limit;
        _window = window;
    }

    public bool IsLimited(string key, out TimeSpan retryAfter)
    {
        retryAfter = TimeSpan.Zero;

        var now = DateTime.UtcNow;

        var entry = _cache.GetOrCreate(key, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _window;
            return new RateLimitEntry
            {
                Timestamp = now,
                Count = 0
            };
        });

        if (now - entry.Timestamp < _window)
        {
            if (entry.Count >= _limit)
            {
                retryAfter = _window - (now - entry.Timestamp);
                return true;
            }

            entry.Count++;
            _cache.Set(key, entry, TimeSpan.FromSeconds(10)); // reset TTL
        }
        else
        {
            _cache.Set(key, new RateLimitEntry { Timestamp = now, Count = 1 }, _window);
        }

        return false;
    }

    private class RateLimitEntry
    {
        public DateTime Timestamp { get; set; }
        public int Count { get; set; }
    }
}