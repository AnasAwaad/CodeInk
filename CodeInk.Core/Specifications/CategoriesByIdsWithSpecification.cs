using CodeInk.Core.Entities;

namespace CodeInk.Core.Specifications;
public class CategoriesByIdsWithSpecification : BaseSpecification<Category>
{
    public CategoriesByIdsWithSpecification(List<int> categoryIds) : base(c => categoryIds.Contains(c.Id))
    {
    }
}
