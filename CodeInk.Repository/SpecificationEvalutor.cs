using CodeInk.Core.Entities;
using CodeInk.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CodeInk.Repository;
public static class SpecificationEvalutor
{
    public static IQueryable<T> GetQuery<T>(IBaseSpecification<T> specification, IQueryable<T> query) where T : BaseEntity
    {
        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        query = specification.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

        return query;
    }
}
