using StackExchange.Redis;
using System.Text.Json;

namespace HackerBestStories.API.Services.Impementation
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly ILogger<RedisCacheService> _logger;

        public RedisCacheService(IConnectionMultiplexer redis, ILogger<RedisCacheService> logger)
        {
            _redis = redis ?? throw new ArgumentNullException(nameof(redis));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            try
            {
                var db = _redis.GetDatabase();
                var value = await db.StringGetAsync(key);

                if (value.IsNullOrEmpty)
                {
                    return default;
                }

                return JsonSerializer.Deserialize<T>(value.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Redis GET failed for key {Key}: {Message}", key, ex.Message);
                return default;
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            try
            {
                var db = _redis.GetDatabase();
                var json = JsonSerializer.Serialize(value);
                if (expiration.HasValue)
                {
                    await db.StringSetAsync(key, (RedisValue)json, expiration.Value);
                }
                else
                {
                    await db.StringSetAsync(key, (RedisValue)json);
                }
                _logger.LogInformation("Cached {Key} in Redis for {Minutes} minutes", key, expiration?.TotalMinutes);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Redis SET failed for key {Key}: {Message}", key, ex.Message);
            }
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                var db = _redis.GetDatabase();
                await db.KeyDeleteAsync(key);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Redis DELETE failed for key {Key}: {Message}", key, ex.Message);
            }
        }

        public async Task<bool> ExistsAsync(string key)
        {
            try
            {
                var db = _redis.GetDatabase();
                return await db.KeyExistsAsync(key);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Redis EXISTS failed for key {Key}: {Message}", key, ex.Message);
                return false;
            }
        }
    }
}
