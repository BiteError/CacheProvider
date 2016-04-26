namespace CacheProvider
{
    public enum CacheOption
    {
        /// <summary>
        /// Store to current application memory
        /// </summary>
        Local,
        /// <summary>
        /// Store to current application memory and synced with other instances
        /// </summary>
        Sync,
        /// <summary>
        /// Store to current application memory and synced with other instances
        /// </summary>
        External
    }
    internal static class CacheSyncronizer
    {
        public static void Invalidate(string key)
        {
            //TODO: sync with Redis, MessageBus etc
        }
    }
}