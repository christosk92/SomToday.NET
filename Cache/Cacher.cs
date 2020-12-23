using System;
using Microsoft.Extensions.Caching.Memory;

namespace SomToday.NET.Cache
{
    public abstract class Cacher
    {
        private IMemoryCache? _cache;
        public TimeSpan DefaultCacheDuration { get; private set; }
        protected Cacher(TimeSpan defaultCacheDuration)
        {
            DefaultCacheDuration = defaultCacheDuration;
        }

        public void SetDefaultCacheDuration(TimeSpan newDuration) 
            => DefaultCacheDuration = newDuration;


        /// <summary>
        /// Sets an item in cache
        /// </summary> 
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="val"></param>
        /// <param name="duration"></param>
        /// <returns>True if item succesfully set. False if not cached.</returns>
        public virtual bool SetCache<T>(
            string cacheKey,
            T val,
            TimeSpan? duration = null)
        {
            _cache ??= new MemoryCache(new MemoryCacheOptions());
            _cache.Set(cacheKey, val, duration ?? DefaultCacheDuration);
            return _cache.TryGetValue(cacheKey, out _);
        }
    }
}
