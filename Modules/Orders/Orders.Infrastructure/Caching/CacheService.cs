using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Orders.Application.Common.Interfaces;

namespace Orders.Infrastructure.Cashing;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CacheService> _logger;

    private static readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();

    public CacheService(IDistributedCache distributedCache)
    {
        _cache = distributedCache;
    }


    public async Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
    {
        var data = await _cache.GetStringAsync(key, ct);

        if (data is null) return default;

        return JsonSerializer.Deserialize<T>(data);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default)
    {
        var options = new DistributedCacheEntryOptions();

        if (ttl.HasValue)
            options.AbsoluteExpirationRelativeToNow = ttl;

        var json = JsonSerializer.Serialize(value);

        await _cache.SetStringAsync(key, json, options, ct);
    }

    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? ttl = null,
        CancellationToken ct = default)
    {
        // Сначала пробуем взять из кэша
        var cached = await GetAsync<T>(key, ct);
        if (cached is not null)
        {
            _logger.LogInformation("Cache hit: {Key}", key);
            return cached;
        }

        _logger.LogInformation("Cache miss: {Key}", key);

        // Лок для предотвращения одновременной записи
        var myLock = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
        await myLock.WaitAsync(ct);

        try
        {
            // Проверяем кэш ещё раз после входа в лок
            cached = await GetAsync<T>(key, ct);
            if (cached is not null)
            {
                _logger.LogInformation("Cache hit (after lock): {Key}", key);
                return cached;
            }

            // Создаём объект через factory
            var value = await factory();

            // Сохраняем в кэш
            await SetAsync(key, value, ttl, ct);

            return value;
        }
        finally
        {
            myLock.Release();
        }
    }

    public Task RemoveAsync(string key, CancellationToken ct = default)
    {
        return _cache.RemoveAsync(key, ct);
    }
}