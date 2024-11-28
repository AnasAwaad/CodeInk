using CodeInk.Core.Entities;
using System.Linq.Expressions;

namespace CodeInk.Core.Specifications;
public class BaseSpecification<T> : IBaseSpecification<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public IList<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
    public IList<string> IncludeStrings { get; set; } = new List<string>();
    public Expression<Func<T, object>> OrderBy { get; set; }
    public Expression<Func<T, object>> OrderByDesc { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }


    // Get All 
    public BaseSpecification()
    {

    }

    // Get By Id
    public BaseSpecification(Expression<Func<T, bool>> criterialExpression)
    {
        Criteria = criterialExpression;
    }


    public void SetOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    public void SetOrderByDesc(Expression<Func<T, object>> orderByExpression)
    {
        OrderByDesc = orderByExpression;
    }

    public void SetPagination(int skip, int take)
    {
        Skip = skip;
        Take = take;
    }
}
