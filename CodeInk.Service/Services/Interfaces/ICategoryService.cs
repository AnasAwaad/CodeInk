using CodeInk.Application.DTOs;
using CodeInk.Application.DTOs.Category;

namespace CodeInk.Application.Services.Interfaces;
public interface ICategoryService
{
    Task<IEnumerable<CategoryToReturnDto>> GetCategoriesAsync();
    Task<CategoryToReturnDto?> GetCategoryByIdAsync(int id);
    Task<ServiceResponse> CreateCategoryAsync(AddCategoryDto categoryDto);
    Task<ServiceResponse> UpdateCategoryAsync(UpdateCategoryDto categoryDto);
    Task<ServiceResponse> RemoveCategoryAsync(int id);

}
