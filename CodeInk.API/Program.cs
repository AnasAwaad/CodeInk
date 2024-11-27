using CodeInk.API.Extensions;
using CodeInk.API.Middlewares;
using CodeInk.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeInk.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);



        #region  Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });


        builder.Services.AddApplicationServices();


        #endregion

        var app = builder.Build();

        #region SeedData
        // group of services lifeTime scooped
        using var scope = app.Services.CreateScope();

        // services its self
        var services = scope.ServiceProvider;

        // ask CLR to create object from class implement interface (ILoggerFactory) explicitly
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();

        try
        {
            // ask CLR to create object from dbContext explicitly
            var dbContext = services.GetRequiredService<AppDbContext>();

            await AppDbContextSeed.SeedDataAsync(dbContext);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An Error Occured During Seed Data");
        }



        #endregion

        #region Configure the HTTP request pipeline.

        app.UseMiddleware<ExceptionMiddleWare>();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();

        // for notfound endpoint
        app.UseStatusCodePagesWithRedirects("/errors/{0}");

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        #endregion

        app.Run();
    }
}
