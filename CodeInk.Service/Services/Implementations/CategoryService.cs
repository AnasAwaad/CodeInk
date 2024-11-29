using AutoMapper;
using CodeInk.Application.DTOs;
using CodeInk.Application.DTOs.Category;
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

    public async Task<ServiceResponse> CreateCategoryAsync(AddCategoryDto categoryDto)
    {
        var spec = new CategoryByNameSpecification(categoryDto.Name);
        var isCategoryExists = await _categoryRepo.IsExistsWithSpecAsync(spec);

        if (isCategoryExists)
            return new ServiceResponse(false, $"Category with the name '{categoryDto.Name}' already exists.");

        var mappedCategory = _mapper.Map<Category>(categoryDto);
        await _categoryRepo.CreateAsync(mappedCategory);

        return new ServiceResponse(true, "Category created successfully");
    }

    public async Task<IEnumerable<CategoryToReturnDto>> GetCategoriesAsync()
    {
        var categorySpec = new CategoryWithBooksSpecification();

        var categories = await _categoryRepo.GetAllWithSpecAsync(categorySpec);

        return _mapper.Map<IEnumerable<CategoryToReturnDto>>(categories);
    }

    public async Task<CategoryToReturnDto?> GetCategoryByIdAsync(int id)
    {
        var categorySpec = new CategoryWithBooksSpecification(id);
        var category = await _categoryRepo.GetByIdWithSpecAsync(categorySpec);

        if (category is null)
            return null;

        return _mapper.Map<CategoryToReturnDto>(category);
    }

    public async Task<ServiceResponse> RemoveCategoryAsync(int id)
    {
        var spec = new CategoryByIdSpecification(id);

        var category = await _categoryRepo.GetByIdWithSpecAsync(spec);

        if (category is null)
            return new ServiceResponse(false, $"Category with Id {id} Not Found");

        await _categoryRepo.DeleteAsync(category);

        return new ServiceResponse(true, "Category removed successfully");
    }

    public async Task<ServiceResponse> UpdateCategoryAsync(UpdateCategoryDto categoryDto)
    {
        var spec = new CategoryByIdSpecification(categoryDto.Id);

        var category = await _categoryRepo.GetByIdWithSpecAsync(spec);

        var isCategoryExists = await _categoryRepo.IsExistsWithSpecAsync(spec);

        if (!isCategoryExists)
            return new ServiceResponse(false, $"Category with ID {categoryDto.Id} not found.");

        var nameSpec = new CategoryByNameSpecification(categoryDto.Name);
        var existingCategory = await _categoryRepo.IsExistsWithSpecAsync(nameSpec);

        if (existingCategory && category.Name != categoryDto.Name)
            return new ServiceResponse(false, $"Category name '{categoryDto.Name}' must be unique.");

        category = _mapper.Map(categoryDto, category);

        await _categoryRepo.UpdateAsync(category);

        return new ServiceResponse(true, "Category updated Successfully");
    }
}
