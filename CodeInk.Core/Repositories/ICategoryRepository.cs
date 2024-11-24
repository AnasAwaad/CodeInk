using CodeInk.Core.Entities;

namespace CodeInk.Core.Repositories;
public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<List<Category>> GetByIdsAsync(List<int> categoryIds);
}
