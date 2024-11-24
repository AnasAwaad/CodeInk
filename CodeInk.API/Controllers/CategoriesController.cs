using AutoMapper;
using CodeInk.API.DTOs;
using CodeInk.API.Errors;
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
    public async Task<ActionResult<CategoryToReturnDto>> GetCategoryById(int id)
    {

        var categorySpec = new CategoryWithBooksSpecification(id);
        var category = await _categoryRepo.GetByIdWithSpecAsync(categorySpec);

        if (category is null)
            return NotFound(new ApiResponse(404));

        var mappedCategory = _mapper.Map<CategoryToReturnDto>(category);

        return Ok(mappedCategory);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> CreateCategory(AddCategoryDto category)
    {
        var spec = new CategoryByNameSpecification(category.Name);
        var isCategoryExists = await _categoryRepo.IsExistsWithSpecAsync(spec);
        if (isCategoryExists)
        {
            return BadRequest(new ApiResponse(400, "Category name must be unique."));
        }

        var mappedCategory = _mapper.Map<Category>(category);
        await _categoryRepo.CreateAsync(mappedCategory);

        return CreatedAtAction(nameof(GetCategoryById), new { Id = mappedCategory.Id }, new ApiResponse(201));
    }


    [HttpPut]
    public async Task<ActionResult<ApiResponse>> UpdateCategory(UpdateCategoryDto category)
    {

        var spec = new CategoryByIdSpecification(category.Id);

        var oldCategory = await _categoryRepo.GetByIdWithSpecAsync(spec);

        var isCategoryExists = await _categoryRepo.IsExistsWithSpecAsync(spec);

        if (!isCategoryExists)
            return NotFound(new ApiResponse(404, "Category Not Found."));

        var nameSpec = new CategoryByNameSpecification(category.Name);
        var existingCategory = await _categoryRepo.IsExistsWithSpecAsync(nameSpec);

        if (existingCategory && oldCategory.Name != category.Name)
        {
            return BadRequest(new ApiResponse(400, "Category name must be unique."));
        }

        _mapper.Map(category, oldCategory);

        await _categoryRepo.UpdateAsync(oldCategory);
        return Ok(new ApiResponse(200, "Category Updated Successfully."));
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> RemoveCategory(int id)
    {
        var spec = new CategoryByIdSpecification(id);

        var category = await _categoryRepo.GetByIdWithSpecAsync(spec);

        if (category is null)
            return NotFound(new ApiResponse(404, "Category Not Found."));

        await _categoryRepo.DeleteAsync(category);

        return Ok(new ApiResponse(200, "Category deleted successfully."));
    }
}
