using CodeInk.API.Errors;
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
        var categories = await _categoryService.GetCategoriesAsync();

        return Ok(categories);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryToReturnDto>> GetCategoryById(int id)
    {

        try
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse(404, ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> CreateCategory(AddCategoryDto category)
    {
        try
        {
            await _categoryService.CreateCategoryAsync(category);
            return Ok(new ApiResponse(201));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse(400, ex.Message));
        }
    }


    [HttpPut]
    public async Task<ActionResult<ApiResponse>> UpdateCategory(UpdateCategoryDto category)
    {

        try
        {
            await _categoryService.UpdateCategoryAsync(category);
            return Ok(new ApiResponse(200, "Category Updated Successfully."));
        }
        catch (Exception ex)
        {
            if (ex.GetType() == typeof(InvalidOperationException))
                return BadRequest(new ApiResponse(400, ex.Message));

            return NotFound(new ApiResponse(404, ex.Message));
        }
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> RemoveCategory(int id)
    {
        try
        {
            await _categoryService.RemoveCategoryAsync(id);
            return Ok(new ApiResponse(200, "Category deleted successfully."));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse(404, ex.Message));
        }

    }
}
