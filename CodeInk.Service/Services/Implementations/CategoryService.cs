using AutoMapper;
using CodeInk.Application.DTOs.Category;
using CodeInk.Application.Services.Interfaces;
using CodeInk.Core.Entities;
using CodeInk.Core.Exceptions;
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

    public async Task<int> CreateCategoryAsync(AddCategoryDto categoryDto)
    {
        var spec = new CategoryByNameSpecification(categoryDto.Name);
        var isCategoryExists = await _categoryRepo.IsExistsWithSpecAsync(spec);

        if (isCategoryExists)
            throw new CategoryNameAlreadyExistsException(categoryDto.Name);

        var mappedCategory = _mapper.Map<Category>(categoryDto);
        await _categoryRepo.CreateAsync(mappedCategory);

        return mappedCategory.Id;
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
            throw new CategoryNotFoundException(id);

        return _mapper.Map<CategoryToReturnDto>(category);
    }

    public async Task RemoveCategoryAsync(int id)
    {
        var spec = new CategoryByIdSpecification(id);

        var category = await _categoryRepo.GetByIdWithSpecAsync(spec);

        if (category is null)
            throw new CategoryNotFoundException(id);

        category.IsActive = false;
        await _categoryRepo.UpdateAsync(category);

    }

    public async Task UpdateCategoryAsync(UpdateCategoryDto categoryDto)
    {
        var spec = new CategoryByIdSpecification(categoryDto.Id);

        var category = await _categoryRepo.GetByIdWithSpecAsync(spec);

        if (category is null)
            throw new CategoryNotFoundException(categoryDto.Id);

        var nameSpec = new CategoryByNameSpecification(categoryDto.Name);

        var existingCategory = await _categoryRepo.IsExistsWithSpecAsync(nameSpec);

        if (existingCategory && category.Id != categoryDto.Id)
            throw new CategoryNameAlreadyExistsException(categoryDto.Name);

        category = _mapper.Map(categoryDto, category);

        await _categoryRepo.UpdateAsync(category);

    }
}
