using CodeInk.Repository.Models;

namespace CodeInk.Core.Repositories;
public interface IBasketRepository
{
    Task<CustomerBasket?> GetBasketAsync(string id);
    Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
    Task<bool> RemoveBasketAsync(string id);
}
