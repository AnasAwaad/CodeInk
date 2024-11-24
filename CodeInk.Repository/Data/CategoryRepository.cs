using CodeInk.Core.Entities;
using CodeInk.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CodeInk.Repository.Data;
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext dbContext) : base(dbContext)
    {

    }
    public async Task<List<Category>> GetByIdsAsync(List<int> categoryIds)
    {
        return await dbContext.Categories
                              .Where(c => categoryIds.Contains(c.Id))
                              .ToListAsync();
    }
}
