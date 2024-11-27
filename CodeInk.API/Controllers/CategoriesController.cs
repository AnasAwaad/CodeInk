using CodeInk.Application.DTOs;
using CodeInk.Application.Services.Interfaces;
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
        var response = await _categoryService.GetCategoriesAsync();
        return StatusCode(response.StatusCode, response);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryToReturnDto>> GetCategoryById(int id)
    {
        var response = await _categoryService.GetCategoryByIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> CreateCategory(AddCategoryDto category)
    {
        var response = await _categoryService.CreateCategoryAsync(category);
        return StatusCode(response.StatusCode, response);
    }


    [HttpPut]
    public async Task<ActionResult<ApiResponse>> UpdateCategory(UpdateCategoryDto category)
    {
        var response = await _categoryService.UpdateCategoryAsync(category);
        return StatusCode(response.StatusCode, response);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> RemoveCategory(int id)
    {
        var response = await _categoryService.RemoveCategoryAsync(id);
        return StatusCode(response.StatusCode, response);
    }
}
