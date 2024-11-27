using AutoMapper;
using CodeInk.Application.DTOs;
using CodeInk.Application.Services.Interfaces;
using CodeInk.Core.Entities;
using CodeInk.Core.Repositories;
using CodeInk.Core.Specifications;

namespace CodeInk.Application.Services.Implementations;
public class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> _categoryRepo;
    private readonly IMapper _mapper;

    public CategoryService(IGenericRepository<Category> categoryRepo, IMapper mapper)
    {
        _categoryRepo = categoryRepo;
        _mapper = mapper;
    }

    public async Task CreateCategoryAsync(AddCategoryDto categoryDto)
    {
        var spec = new CategoryByNameSpecification(categoryDto.Name);
        var isCategoryExists = await _categoryRepo.IsExistsWithSpecAsync(spec);

        if (isCategoryExists)
            throw new InvalidOperationException("Category name must be unique.");

        var mappedCategory = _mapper.Map<Category>(categoryDto);
        await _categoryRepo.CreateAsync(mappedCategory);
    }

    public async Task<IEnumerable<CategoryToReturnDto>> GetCategoriesAsync()
    {
        var categorySpec = new CategoryWithBooksSpecification();

        var categories = await _categoryRepo.GetAllWithSpecAsync(categorySpec);

        var mappedCategories = _mapper.Map<IEnumerable<CategoryToReturnDto>>(categories);

        return mappedCategories;
    }

    public async Task<CategoryToReturnDto> GetCategoryByIdAsync(int id)
    {
        var categorySpec = new CategoryWithBooksSpecification(id);
        var category = await _categoryRepo.GetByIdWithSpecAsync(categorySpec);

        if (category is null)
            throw new KeyNotFoundException("Category Not Found.");

        return _mapper.Map<CategoryToReturnDto>(category);
    }

    public async Task RemoveCategoryAsync(int id)
    {
        var spec = new CategoryByIdSpecification(id);

        var category = await _categoryRepo.GetByIdWithSpecAsync(spec);

        if (category is null)
            throw new KeyNotFoundException("Category Not Found.");

        await _categoryRepo.DeleteAsync(category);
    }

    public async Task UpdateCategoryAsync(UpdateCategoryDto categoryDto)
    {
        var spec = new CategoryByIdSpecification(categoryDto.Id);

        var category = await _categoryRepo.GetByIdWithSpecAsync(spec);

        var isCategoryExists = await _categoryRepo.IsExistsWithSpecAsync(spec);

        if (!isCategoryExists)
            throw new KeyNotFoundException("Category Not Found.");

        var nameSpec = new CategoryByNameSpecification(categoryDto.Name);
        var existingCategory = await _categoryRepo.IsExistsWithSpecAsync(nameSpec);

        if (existingCategory && category.Name != categoryDto.Name)
            throw new InvalidOperationException("Category name must be unique.");

        category = _mapper.Map(categoryDto, category);

        await _categoryRepo.UpdateAsync(category);
    }
}
