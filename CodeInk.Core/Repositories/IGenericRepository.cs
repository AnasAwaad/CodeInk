using CodeInk.Core.Entities;
using CodeInk.Core.Specifications;

namespace CodeInk.Core.Repositories;
public interface IGenericRepository<T> where T : BaseEntity
{

    #region Without Sepcification
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    #endregion

    #region Specification
    Task<IEnumerable<T>> GetAllWithSpecAsync(IBaseSpecification<T> spec);
    Task<T> GetByIdWithSpecAsync(IBaseSpecification<T> spec);
    #endregion
}
