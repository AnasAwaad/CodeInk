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

    public async Task CreateAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }
    #endregion

    #region Specification
    public async Task<IEnumerable<T>> GetAllWithSpecAsync(IBaseSpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }


    public async Task<T> GetByIdWithSpecAsync(IBaseSpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<bool> IsExistsWithSpecAsync(IBaseSpecification<T> spec)
    {
        return await ApplySpecification(spec).AnyAsync();
    }


    private IQueryable<T> ApplySpecification(IBaseSpecification<T> spec)
    {
        return SpecificationEvalutor.GetQuery(spec, _dbContext.Set<T>());
    }
    #endregion



}
