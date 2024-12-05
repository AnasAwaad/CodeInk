using CodeInk.API.Extensions;
using CodeInk.API.Middlewares;
using CodeInk.Core.Entities.IdentityEntities;
using CodeInk.Repository.Data;
using CodeInk.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

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

        builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
        {
            var connection = builder.Configuration.GetConnectionString("RedisConnection");
            return ConnectionMultiplexer.Connect(connection);
        });

        builder.Services.AddDbContext<AppIdentityDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
        });

        builder.Services.AddApplicationServices()
                        .AddIdentityServices(builder.Configuration);

        // Add CORS configuration
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.WithOrigins("*")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });
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
            var identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
            var userManage = services.GetRequiredService<UserManager<ApplicationUser>>();


            await dbContext.Database.MigrateAsync();
            await identityDbContext.Database.MigrateAsync();

            await AppIdentityDbSeed.SeedUsersAsync(userManage);
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

        // Apply CORS policy
        app.UseCors("CorsPolicy");

        app.UseAuthorization();


        app.MapControllers();

        #endregion

        app.Run();
    }
}
