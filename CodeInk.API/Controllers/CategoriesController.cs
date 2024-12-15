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

    [Authorize(Roles = "Admin")]
    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<CategoryToReturnDto>>> GetAllCategories()
    {
        var data = await _categoryService.GetAllCategoriesAsync(false);
        return Ok(new ApiResponse(200, "Categories retrived successfully", data));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryToReturnDto>>> GetAllActiveCategories()
    {
        var data = await _categoryService.GetAllCategoriesAsync();
        return Ok(new ApiResponse(200, "Categories retrived successfully", data));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryToReturnDto>> GetCategoryById(int id)
    {
        var data = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(new ApiResponse(200, "Category retrived successfully", data));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ApiResponse>> CreateCategory(AddCategoryDto category)
    {
        var categoryId = await _categoryService.CreateCategoryAsync(category);
        return Ok(new ApiResponse(201, "Category created successfully", new { categoryId }));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<ApiResponse>> UpdateCategory(UpdateCategoryDto category)
    {
        await _categoryService.UpdateCategoryAsync(category);
        return Ok(new ApiResponse(200, "Category updated successfully"));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> RemoveCategory(int id)
    {
        await _categoryService.RemoveCategoryAsync(id);
        return Ok(new ApiResponse(200, "Category removed successfully"));

    }
}
