using CodeInk.Application.DTOs.Category;

namespace CodeInk.Application.Services.Interfaces;
public interface ICategoryService
{
    Task<IEnumerable<CategoryToReturnDto>> GetCategoriesAsync();
    Task<CategoryToReturnDto?> GetCategoryByIdAsync(int id);
    Task<int> CreateCategoryAsync(AddCategoryDto categoryDto);
    Task UpdateCategoryAsync(UpdateCategoryDto categoryDto);
    Task RemoveCategoryAsync(int id);

}
