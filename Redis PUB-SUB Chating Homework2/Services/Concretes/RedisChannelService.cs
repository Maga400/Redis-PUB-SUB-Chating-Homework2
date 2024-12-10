using Redis_PUB_SUB_Chating_Homework2.Services.Abstracts;
using StackExchange.Redis;

namespace Redis_PUB_SUB_Chating_Homework2.Services.Concretes
{
    public class RedisChannelService : IRedisChannelService
    {
        private readonly IConnectionMultiplexer _redis;
        public RedisChannelService(IConnectionMultiplexer redis)
        {
            _redis = redis; 
        }
        public async Task CreateChannelAsync(string channelName)
        {
            if (!string.IsNullOrWhiteSpace(channelName))
            {
                var db = _redis.GetDatabase();
                await db.SetAddAsync("channels", channelName);
            }
        }
        public async Task<List<string>> GetAllChannelsAsync()
        {
            var db = _redis.GetDatabase();
            var channels = await db.SetMembersAsync("channels");
            return channels.Select(c => c.ToString()).ToList();
        }
        public async Task AddMessageToChannelAsync(string channelName, string message)
        {
            if (!string.IsNullOrWhiteSpace(channelName) && !string.IsNullOrWhiteSpace(message))
            {
                var db = _redis.GetDatabase();
                await db.ListRightPushAsync($"channel:{channelName}:messages", message);
            }
        }
        public async Task<List<string>> GetChannelMessagesAsync(string channelName)
        {
            var db = _redis.GetDatabase();
            var messages = await db.ListRangeAsync($"channel:{channelName}:messages");
            return messages.Select(m => m.ToString()).ToList();
        }
    }
}
