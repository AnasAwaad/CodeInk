using CodeInk.Core.Entities;
using CodeInk.Core.Repositories;
using CodeInk.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeInk.Repository;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _dbContext;

    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();

    public async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

}
