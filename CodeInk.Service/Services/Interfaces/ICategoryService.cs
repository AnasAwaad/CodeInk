using CodeInk.Application.DTOs;

namespace CodeInk.Application.Services.Interfaces;
public interface ICategoryService
{
    Task<ApiResponse> GetCategoriesAsync();
    Task<ApiResponse> GetCategoryByIdAsync(int id);
    Task<ApiResponse> CreateCategoryAsync(AddCategoryDto categoryDto);
    Task<ApiResponse> UpdateCategoryAsync(UpdateCategoryDto categoryDto);
    Task<ApiResponse> RemoveCategoryAsync(int id);

}
