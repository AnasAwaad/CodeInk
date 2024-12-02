using CodeInk.Core.Entities;
using System.Linq.Expressions;

namespace CodeInk.Core.Specifications;
public interface IBaseSpecification<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public Expression<Func<T, object>> OrderBy { get; set; }
    public Expression<Func<T, object>> OrderByDesc { get; set; }
    public IList<Expression<Func<T, object>>> Includes { get; set; }
    public IList<string> IncludeStrings { get; set; }
    public bool IsPaginationEnabled { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
}
