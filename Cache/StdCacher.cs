using System;
using Microsoft.Extensions.Caching.Memory;

namespace SomToday.NET.Cache
{
    public class StdCacher : Cacher
    {
        private readonly TimeSpan _defaultCacheDuration;
        public StdCacher(TimeSpan defaultCacheDuration) 
            : base(defaultCacheDuration)
        {
            _defaultCacheDuration = defaultCacheDuration;
        }

        public override bool SetCache<T>(string cacheKey, T val, TimeSpan? duration = null)
        {
            return base.SetCache(cacheKey, 
                val, 
                duration ?? _defaultCacheDuration);
        }
    }
}
