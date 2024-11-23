using CodeInk.Core.Entities;

namespace CodeInk.Core.Specifications;
public class CategoryByIdSpecification : BaseSpecification<Category>
{
    public CategoryByIdSpecification(int id) : base(c => c.Id == id)
    {

    }
}
