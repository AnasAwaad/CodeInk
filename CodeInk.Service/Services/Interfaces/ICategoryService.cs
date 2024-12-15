using CodeInk.Application.DTOs.Category;

namespace CodeInk.Application.Services.Interfaces;
public interface ICategoryService
{
    Task<IEnumerable<CategoryToReturnDto>> GetAllCategoriesAsync(bool applyActiveFilteration = true);
    Task<CategoryToReturnDto?> GetCategoryByIdAsync(int id, bool applyActiveFilteration = true);
    Task<int> CreateCategoryAsync(AddCategoryDto categoryDto);
    Task UpdateCategoryAsync(UpdateCategoryDto categoryDto);
    Task RemoveCategoryAsync(int id);

}
