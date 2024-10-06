using API.Repository.Database.Repository;
using API.Repository.LeftMenu;
using API.Repository.Procedure.Repository;
using API.Repository.Table;
using Microsoft.Extensions.Caching.Distributed;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Clear default configuration sources and setup custom configurations
        ConfigureAppSettings(builder, args);

        // Configure Redis Cache
        ConfigureRedisCache(builder);

        // Configure repositories with generic registration method
        RegisterRepositories(builder);

        // Configure CORS
        ConfigureCors(builder);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Setup middlewares
        SetupMiddlewares(app);

        app.Run();
    }

    private static void ConfigureAppSettings(WebApplicationBuilder builder, string[] args)
    {
        builder.Configuration.Sources.Clear();

        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args);
    }

    private static void ConfigureRedisCache(WebApplicationBuilder builder)
    {
        var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection");

        // Validate Redis connection string
        if (string.IsNullOrEmpty(redisConnectionString))
        {
            throw new ArgumentNullException("Redis connection string is required.");
        }

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
            options.InstanceName = "mssqlInstance:";
        });
    }

    private static void RegisterRepositories(WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");

        // Generic repository registration to avoid repetitive code
        RegisterRepository<DatabaseRepository>(builder, connectionString);
        RegisterRepository<LeftMenuRepository>(builder, connectionString);
        RegisterRepository<TableRepository>(builder, connectionString);
        RegisterRepository<TablesRepository>(builder, connectionString);
        RegisterRepository<ProcedureRepository>(builder, connectionString);
    }

    private static void RegisterRepository<T>(WebApplicationBuilder builder, string connectionString) where T : class
    {
        builder.Services.AddScoped(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<T>>();
            var cache = provider.GetRequiredService<IDistributedCache>();
            return Activator.CreateInstance(typeof(T), connectionString, logger, cache) as T;
        });
    }

    private static void ConfigureCors(WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", policyBuilder =>
            {
                policyBuilder.WithOrigins("http://localhost:4200") // Update URL as necessary
                              .AllowAnyMethod()
                              .AllowAnyHeader();
            });
        });
    }

    private static void SetupMiddlewares(WebApplication app)
    {
        app.UseCors("AllowOrigin");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();
    }
}
