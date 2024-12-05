using CodeInk.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeInk.Repository.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<BookCategory> BookCategories { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
}
