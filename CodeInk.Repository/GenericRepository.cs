using CodeInk.Core.Entities;
using CodeInk.Core.Repositories;
using CodeInk.Core.Specifications;
using CodeInk.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeInk.Repository;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext dbContext;

    public GenericRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    #region Without Sepcification
    public async Task<IEnumerable<T>> GetAllAsync() => await dbContext.Set<T>().ToListAsync();
    public async Task<T> GetByIdAsync(int id) => await dbContext.Set<T>().FindAsync(id);

    public async Task CreateAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        dbContext.Set<T>().Update(entity);
        await dbContext.SaveChangesAsync();
    }


    public async Task DeleteAsync(T entity)
    {
        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
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
        return SpecificationEvalutor.GetQuery(spec, dbContext.Set<T>());
    }
    #endregion



}
