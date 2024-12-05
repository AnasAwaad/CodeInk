using CodeInk.Application.DTOs;
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

        return data is null ? NotFound(new ApiResponse(404, $"Category with Id {id} Not Found"))
                              : Ok(new ApiResponse(200, "Categories retrived successfully", data));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ApiResponse>> CreateCategory(AddCategoryDto category)
    {
        var result = await _categoryService.CreateCategoryAsync(category);

        return result.success ? Ok(new ApiResponse(201, result.message))
                              : BadRequest(new ApiResponse(300, result.message));
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<ApiResponse>> UpdateCategory(UpdateCategoryDto category)
    {
        var result = await _categoryService.UpdateCategoryAsync(category);

        return result.success ? Ok(new ApiResponse(201, result.message))
                              : BadRequest(new ApiResponse(300, result.message));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> RemoveCategory(int id)
    {
        var result = await _categoryService.RemoveCategoryAsync(id);

        return result.success ? Ok(new ApiResponse(201, result.message))
                              : BadRequest(new ApiResponse(300, result.message));
    }
}
