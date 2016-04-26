using System;

namespace CacheProvider
{
    internal class CacheValue
    {
        public readonly DateTime ExpirationDate;
        public readonly object Value;
        public readonly CacheOption Option;

        public CacheValue(object value, DateTime expirationDate, CacheOption option)
        {
            Value = value;
            ExpirationDate = expirationDate;
            Option = option;
        }
    }
}