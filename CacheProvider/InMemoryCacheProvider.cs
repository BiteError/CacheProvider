using System;
using System.Threading;

namespace CacheProvider
{
    class InMemoryCacheProvider : ICacheProvider
    {
        /// <summary>
        /// lifetime in second
        /// </summary>
        private const int Duration = 120;

        private static DateTime ExpirationDate => DateTime.Now.AddSeconds(Duration);

        private static string GetKeyForCache<T>(string id)
        {
            return $"{id}{typeof (T).Name}";
        }

        public T Get<T>(string id, Func<string, T> func, CacheOption option = CacheOption.Local)
        {
            T value;
            var keyForCache = GetKeyForCache<T>(id);
            if (CacheContainer.Cache.ContainsKey(keyForCache))
            {
                var casheValue = CacheContainer.Cache[keyForCache];

                if (casheValue.ExpirationDate < DateTime.Now)
                {
                    Monitor.Enter(casheValue);
                    try
                    {
                        if (CacheContainer.Cache.ContainsKey(keyForCache) && CacheContainer.Cache[keyForCache].ExpirationDate > DateTime.Now)
                        {
                            value = (T)CacheContainer.Cache[keyForCache].Value;
                        }
                        else
                        {
                            value = GetActualValue(keyForCache, id, func, option);
                        }
                    }
                    finally
                    {
                        Monitor.Exit(casheValue);
                    }
                }
                else
                {
                    value = (T)casheValue.Value;
                }
            }
            else
            {
                var casheValue = CacheContainer.Cache.GetOrAdd
                    (keyForCache,
                        key =>
                            new CacheValue(func.Invoke(id), ExpirationDate, option)
                    );

                value = (T)casheValue.Value;
            }
            return value;
        }

        public void Invalidate<T>(string id)
        {
            var keyForCache = GetKeyForCache<T>(id);
            CacheContainer.InvalidateByKey(keyForCache);
        }

        private static T GetActualValue<T>(string key, string id, Func<string, T> func, CacheOption option)
        {
            var value = func.Invoke(id);

            if (value != null)
            {
                CacheContainer.AddToCache(key, option, value);
            }

            return value;
        }
    }
}