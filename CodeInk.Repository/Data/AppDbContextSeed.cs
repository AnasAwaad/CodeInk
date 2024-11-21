using CodeInk.Core.Entities;
using System.Text.Json;

namespace CodeInk.Repository.Data;
public static class AppDbContextSeed
{
    public static async Task SeedDataAsync(AppDbContext dbContext)
    {

        // Seed categories data
        if (!dbContext.Categories.Any())
        {
            var categoriesData = File.ReadAllText("../CodeInk.Repository/Data/DataSeed/Categories.json");
            var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

            if (categories?.Count > 0)
            {
                foreach (var category in categories)
                {
                    await dbContext.Set<Category>().AddAsync(category);
                }
                await dbContext.SaveChangesAsync();
            }

        }


        // seed books data
        if (!dbContext.Books.Any())
        {
            var BooksData = File.ReadAllText("../CodeInk.Repository/Data/DataSeed/Books.json");
            var books = JsonSerializer.Deserialize<List<Book>>(BooksData);

            if (books?.Count > 0)
            {
                foreach (var book in books)
                {
                    await dbContext.Set<Book>().AddAsync(book);
                }
                await dbContext.SaveChangesAsync();
            }

        }

        // seed bookCategory data
        if (!dbContext.BookCategories.Any())
        {
            var BookCategoryData = File.ReadAllText("../CodeInk.Repository/Data/DataSeed/BookCategories.json");
            var BookCategories = JsonSerializer.Deserialize<List<BookCategory>>(BookCategoryData);

            if (BookCategories?.Count > 0)
            {
                foreach (var bookCategory in BookCategories)
                {
                    await dbContext.Set<BookCategory>().AddAsync(bookCategory);
                }
                await dbContext.SaveChangesAsync();
            }

        }
    }
}
