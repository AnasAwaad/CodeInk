using CodeInk.Core.Entities;
using System.Linq.Expressions;

namespace CodeInk.Core.Specifications;
public class BaseSpecification<T> : IBaseSpecification<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public IList<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();


    // Get All 
    public BaseSpecification()
    {

    }

    // Get By Id
    public BaseSpecification(Expression<Func<T, bool>> criterialExpression)
    {
        Criteria = criterialExpression;
    }
}
