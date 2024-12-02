using CodeInk.Core.Entities;

namespace CodeInk.Core.Specifications;
public class CategoryByNameSpecification : BaseSpecification<Category>
{
    public CategoryByNameSpecification(string name) : base(c => c.Name == name)
    {

    }
}
