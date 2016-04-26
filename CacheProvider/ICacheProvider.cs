using System;

namespace CacheProvider
{
    public interface ICacheProvider
    {
        void Invalidate<T>(string id);
        T Get<T>(string id, Func<string, T> func, CacheOption option = CacheOption.Local);
    }
}
