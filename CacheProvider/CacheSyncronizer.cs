using RedisClient;

namespace CacheProvider
{
    internal static class CacheSyncronizer
    {
        internal static class CacheSyncronizer
        {
            public const string InvalidateChannelName = "CacheInvalidate";

            public static MessageChannel MessageChannel;

            public static void InitializeMessageChannel(RedisClient redisClient)
            {
                if (MessageChannel == null)
                {
                    MessageChannel = redisClient.GetMessageChannel(InvalidateChannelName);
                    //todo check servers looping
                    MessageChannel.Subscrube(CacheContainer.InvalidateByKey);
                }
            }

            public static void Invalidate(string key)
            {
                MessageChannel.Publish(key);
            }
        }
    }
}