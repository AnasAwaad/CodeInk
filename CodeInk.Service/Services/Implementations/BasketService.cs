using AutoMapper;
using CodeInk.Core.Repositories;
using CodeInk.Repository.Models;
using CodeInk.Service.DTOs.Basket;
using CodeInk.Service.Services.Interfaces;

namespace CodeInk.Service.Services.Implementations;
public class BasketService : IBasketService
{
    private readonly IBasketRepository _basketRepo;
    private readonly IMapper _mapper;

    public BasketService(IBasketRepository basketRepo, IMapper mapper)
    {
        _basketRepo = basketRepo;
        _mapper = mapper;
    }
    public async Task<CustomerBasketDto> GetBasketAsync(string basketId)
    {
        var basket = await _basketRepo.GetBasketAsync(basketId);

        if (basket is null)
            return new CustomerBasketDto();

        return _mapper.Map<CustomerBasketDto>(basket);
    }

    public async Task<bool> RemoveBasketAsync(string basketId)
    {
        return await _basketRepo.RemoveBasketAsync(basketId);
    }

    public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket)
    {
        // generate id for create new basket
        if (basket.Id is null)
            basket.Id = GenerateRandomBasketId();

        var mappedBasket = _mapper.Map<CustomerBasket>(basket);

        var updatedBasket = await _basketRepo.UpdateBasketAsync(mappedBasket);

        return _mapper.Map<CustomerBasketDto>(updatedBasket);
    }

    private string GenerateRandomBasketId()
    {
        var random = new Random();

        // generate 4 digit number from 1000 to 9999
        var randNumber = random.Next(1000, 10000);

        return $"BS-{randNumber}";
    }
}
