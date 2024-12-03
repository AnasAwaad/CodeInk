﻿using CodeInk.Core.Entities;
using CodeInk.Core.Repositories;
using StackExchange.Redis;
using System.Text.Json;

namespace CodeInk.Repository;
public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;

    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }
    public async Task<CustomerBasket?> GetBasketAsync(string id)
    {
        var basket = await _database.StringGetAsync(id);

        return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);
    }

    public async Task<bool> RemoveBasketAsync(string id)
    {
        return await _database.KeyDeleteAsync(id);
    }


    // create or update basket
    public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
    {
        var jsonBasket = JsonSerializer.Serialize(basket);

        var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket, TimeSpan.FromDays(1));

        return isCreatedOrUpdated ? await GetBasketAsync(basket.Id)
                                  : null;
    }
}