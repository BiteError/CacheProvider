using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CacheProvider
{
    internal static class CacheContainer
    {
        public const int Duration = 120;
        public static readonly object CacheActualizedLock = new object();
        public static readonly object CacheNewLock = new object();
        public static readonly ConcurrentDictionary<string, CacheValue> Cache = new ConcurrentDictionary<string, CacheValue>();
        public static readonly List<string> InActualize = new List<string>();

        public static void InvalidateByKey(string keyForCache)
        {
            if (Cache.ContainsKey(keyForCache))
            {
                CacheValue v;
                Cache.TryRemove(keyForCache, out v);
                CacheSyncronizer.Invalidate(keyForCache);
            }
        }

        public static void AddToCache<T>(string key, CacheOption option, T value)
        {
            Cache[key] = new CacheValue(value, DateTime.Now.AddSeconds(Duration), option);
        }
    }
}