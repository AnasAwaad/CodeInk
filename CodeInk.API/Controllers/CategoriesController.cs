using AutoMapper;
using CodeInk.API.DTOs;
using CodeInk.Core.Entities;
using CodeInk.Core.Repositories;
using CodeInk.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace CodeInk.API.Controllers;

public class CategoriesController : APIBaseController
{

    private readonly IGenericRepository<Category> _categoryRepo;
    private readonly IMapper _mapper;

    public CategoriesController(IGenericRepository<Category> categoryRepo, IMapper mapper)
    {
        _categoryRepo = categoryRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryToReturnDto>>> GetCategories()
    {
        var categorySpec = new CategoryWithBooksSpecification();
        var categories = await _categoryRepo.GetAllWithSpecAsync(categorySpec);
        var mappedCategories = _mapper.Map<IEnumerable<CategoryToReturnDto>>(categories);

        return Ok(mappedCategories);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryById(int id)
    {

        var categorySpec = new CategoryWithBooksSpecification(id);
        var category = await _categoryRepo.GetByIdWithSpecAsync(categorySpec);
        var mappedCategory = _mapper.Map<CategoryToReturnDto>(category);

        return Ok(mappedCategory);
    }
}
