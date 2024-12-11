using CodeInk.Core.Entities;
using CodeInk.Core.Specifications;

namespace CodeInk.Core.Repositories;
public interface IGenericRepository<T> where T : BaseEntity
{

    #region Without Sepcification
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> CountAllAsync();
    #endregion

    #region Specification
    Task<IEnumerable<T>> GetAllWithSpecAsync(IBaseSpecification<T> spec);
    Task<T> GetWithSpecAsync(IBaseSpecification<T> spec);
    Task<bool> IsExistsWithSpecAsync(IBaseSpecification<T> spec);
    Task<int> CountWithSpecAsync(IBaseSpecification<T> spec);
    #endregion
}
