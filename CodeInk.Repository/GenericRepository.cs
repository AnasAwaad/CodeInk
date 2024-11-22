using CodeInk.Core.Entities;
using CodeInk.Core.Repositories;
using CodeInk.Core.Specifications;
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

    #region Without Sepcification
    public async Task<IEnumerable<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();
    public async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

    #endregion

    public async Task<IEnumerable<T>> GetAllWithSpecAsync(IBaseSpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }


    public async Task<T> GetByIdWithSpecAsync(IBaseSpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }


    private IQueryable<T> ApplySpecification(IBaseSpecification<T> spec)
    {
        return SpecificationEvalutor.GetQuery(spec, _dbContext.Set<T>());
    }
}
