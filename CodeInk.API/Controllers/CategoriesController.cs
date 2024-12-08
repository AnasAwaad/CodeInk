using CodeInk.API.Errors;
using CodeInk.Application.DTOs.Category;
using CodeInk.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeInk.API.Controllers;

public class CategoriesController : APIBaseController
{

    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryToReturnDto>>> GetCategories()
    {
        var data = await _categoryService.GetCategoriesAsync();
        return Ok(new ApiResponse(200, "Categories retrived successfully", data));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryToReturnDto>> GetCategoryById(int id)
    {
        var data = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(new ApiResponse(200, "Category retrived successfully", data));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ApiResponse>> CreateCategory(AddCategoryDto category)
    {
        var categoryId = await _categoryService.CreateCategoryAsync(category);
        return Ok(new ApiResponse(201, "Category created successfully", new { categoryId }));
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<ApiResponse>> UpdateCategory(UpdateCategoryDto category)
    {
        await _categoryService.UpdateCategoryAsync(category);
        return Ok(new ApiResponse(200, "Category updated successfully"));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> RemoveCategory(int id)
    {
        await _categoryService.RemoveCategoryAsync(id);
        return Ok(new ApiResponse(200, "Category removed successfully"));

    }
}
