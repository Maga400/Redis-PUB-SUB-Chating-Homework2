using StackExchange.Redis;

namespace Redis_PUB_SUB_Chating_Homework2.Services.Abstracts
{
    public interface IRedisChannelService
    {
        Task CreateChannelAsync(string channelName);
        Task<List<string>> GetAllChannelsAsync();
        Task AddMessageToChannelAsync(string channelName, string message);
        Task<List<string>> GetChannelMessagesAsync(string channelName);
    }
}
