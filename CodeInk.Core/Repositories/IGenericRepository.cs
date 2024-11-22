using CodeInk.Core.Entities;

namespace CodeInk.Core.Repositories;
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
}
