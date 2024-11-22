using CodeInk.Core.Entities;
using CodeInk.Core.Repositories;
using CodeInk.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace CodeInk.API.Controllers;

public class CategoriesController : APIBaseController
{

    private readonly IGenericRepository<Category> _categoryRepo;

    public CategoriesController(IGenericRepository<Category> categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var categorySpec = new BaseSpecification<Category>();
        return Ok(await _categoryRepo.GetAllWithSpecAsync(categorySpec));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryById(int id)
    {

        var categorySpec = new CategoryWithBooksSpecification(id);
        return Ok(await _categoryRepo.GetByIdWithSpecAsync(categorySpec));
    }
}
