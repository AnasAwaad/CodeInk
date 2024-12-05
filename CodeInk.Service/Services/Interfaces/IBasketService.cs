using CodeInk.Service.DTOs.Basket;

namespace CodeInk.Service.Services.Interfaces;
public interface IBasketService
{
    Task<CustomerBasketDto> GetBasketAsync(string basketId);
    Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket);
    Task<bool> RemoveBasketAsync(string basketId);

}
