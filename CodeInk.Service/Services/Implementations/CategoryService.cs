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

    public async Task<ApiResponse> CreateCategoryAsync(AddCategoryDto categoryDto)
    {
        var spec = new CategoryByNameSpecification(categoryDto.Name);
        var isCategoryExists = await _categoryRepo.IsExistsWithSpecAsync(spec);

        if (isCategoryExists)
            return new ApiResponse(409, $"Category with the name '{categoryDto.Name}' already exists.");

        var mappedCategory = _mapper.Map<Category>(categoryDto);
        await _categoryRepo.CreateAsync(mappedCategory);

        return new ApiResponse(201, "Category created successfully");
    }

    public async Task<ApiResponse> GetCategoriesAsync()
    {
        var categorySpec = new CategoryWithBooksSpecification();

        var categories = await _categoryRepo.GetAllWithSpecAsync(categorySpec);

        var mappedCategories = _mapper.Map<IEnumerable<CategoryToReturnDto>>(categories);

        return new ApiResponse(200, "Categories retrieved successfully.", mappedCategories);
    }

    public async Task<ApiResponse> GetCategoryByIdAsync(int id)
    {
        var categorySpec = new CategoryWithBooksSpecification(id);
        var category = await _categoryRepo.GetByIdWithSpecAsync(categorySpec);

        if (category is null)
            return new ApiResponse(404, $"Category with Id {id} Not Found");

        var mappedCategory = _mapper.Map<CategoryToReturnDto>(category);

        return new ApiResponse(200, "Categories retrived successfully", mappedCategory);
    }

    public async Task<ApiResponse> RemoveCategoryAsync(int id)
    {
        var spec = new CategoryByIdSpecification(id);

        var category = await _categoryRepo.GetByIdWithSpecAsync(spec);

        if (category is null)
            return new ApiResponse(404, $"Category with Id {id} Not Found");

        await _categoryRepo.DeleteAsync(category);

        return new ApiResponse(200, "Category removed successfully");
    }

    public async Task<ApiResponse> UpdateCategoryAsync(UpdateCategoryDto categoryDto)
    {
        var spec = new CategoryByIdSpecification(categoryDto.Id);

        var category = await _categoryRepo.GetByIdWithSpecAsync(spec);

        var isCategoryExists = await _categoryRepo.IsExistsWithSpecAsync(spec);

        if (!isCategoryExists)
            return new ApiResponse(404, $"Category with ID {categoryDto.Id} not found.");

        var nameSpec = new CategoryByNameSpecification(categoryDto.Name);
        var existingCategory = await _categoryRepo.IsExistsWithSpecAsync(nameSpec);

        if (existingCategory && category.Name != categoryDto.Name)
            return new ApiResponse(400, $"Category name '{categoryDto.Name}' must be unique.");

        category = _mapper.Map(categoryDto, category);

        await _categoryRepo.UpdateAsync(category);

        return new ApiResponse(200, "Category updated Successfully");
    }
}
