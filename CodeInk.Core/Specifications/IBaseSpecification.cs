using CodeInk.Core.Entities;
using System.Linq.Expressions;

namespace CodeInk.Core.Specifications;
public interface IBaseSpecification<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public IList<Expression<Func<T, object>>> Includes { get; set; }
}
