using Microsoft.Extensions.Caching.Memory;

namespace CasaEscuela.AppWebMVC.Models
{
    public class CacheService
    {
        private readonly IMemoryCache _cache;
        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public bool Exists(string name)
        {
            return _cache.TryGetValue(name, out _);
        }
        public void Set(string name, string value,TimeSpan timeSpan)
        {            
            _cache.Set(name, value, timeSpan);
        }

        public string? Get(string name)
        {            
            _cache.TryGetValue(name, out string? value);
            return value;
        }
    }
}
