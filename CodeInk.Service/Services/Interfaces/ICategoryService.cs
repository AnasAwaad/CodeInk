using CodeInk.Application.DTOs;

namespace CodeInk.Application.Services.Interfaces;
public interface ICategoryService
{
    Task<IEnumerable<CategoryToReturnDto>> GetCategoriesAsync();
    Task<CategoryToReturnDto> GetCategoryByIdAsync(int id);
    Task CreateCategoryAsync(AddCategoryDto categoryDto);
    Task UpdateCategoryAsync(UpdateCategoryDto categoryDto);
    Task RemoveCategoryAsync(int id);

}
