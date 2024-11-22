using CodeInk.Core.Entities;
using CodeInk.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CodeInk.Repository;
public static class SpecificationEvalutor
{
    public static IQueryable<T> GetQuery<T>(IBaseSpecification<T> specification, IQueryable<T> query) where T : BaseEntity
    {
        // Apply criteria
        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        // Apply includes
        query = specification.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

        // Apply string-based includes (for ThenInclude scenarios)
        query = specification.IncludeStrings.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

        return query;
    }
}
