using CodeInk.Service.Services.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace CodeInk.Service.Services.Implementations;
public class CacheService : ICacheService
{
    private readonly IDatabase _database;
    public CacheService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }
    public async Task<string?> GetCacheResponseAsync(string key)
    {
        var cachedResponse = await _database.StringGetAsync(key);

        return cachedResponse.IsNull ? null : cachedResponse.ToString();
    }

    public async Task SetCacheResponseAsync(string key, object response, TimeSpan timeToLive)
    {
        if (response is null)
            return;

        var serializedResponse = JsonSerializer.Serialize(response);

        await _database.StringSetAsync(key, serializedResponse, timeToLive);
    }
}
