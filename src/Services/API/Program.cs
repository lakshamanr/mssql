using API.Repository.Database.Repository;
using API.Repository.LeftMenu;
using API.Repository.Table;
using Microsoft.Extensions.Caching.Distributed;
internal class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.Sources.Clear();

        builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);


        var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection");

        // Check if the connection string is null or empty
        if (string.IsNullOrEmpty(redisConnectionString))
        {
            throw new ArgumentNullException("Redis connection string cannot be null or empty.");
        }


        // Configure Redis caching
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
            options.InstanceName = "mssqlInstance:";
        });
        var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");

        builder.Services.AddScoped(provider =>
        {

            var logger = provider.GetRequiredService<ILogger<DatabaseReposititory>>();
            var cache = provider.GetRequiredService<IDistributedCache>();
            return new DatabaseReposititory(connectionString, logger, cache);
        });
        builder.Services.AddScoped(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<LeftMenuRepository>>();
            var cache = provider.GetRequiredService<IDistributedCache>();
            return new LeftMenuRepository(connectionString, logger, cache);
        });

        builder.Services.AddScoped(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<TableRepository>>();
            var cache = provider.GetRequiredService<IDistributedCache>();
            return new TableRepository(connectionString, logger, cache);
        });

        builder.Services.AddScoped(provider =>
        {

            var logger = provider.GetRequiredService<ILogger<TableRepository>>();
            var cache = provider.GetRequiredService<IDistributedCache>();
            return new TablesRepository(connectionString, logger, cache);
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin",
                   builder => builder.WithOrigins("http://localhost:4200") // or the URL of your Angular app
                                     .AllowAnyMethod()
                                     .AllowAnyHeader());
        });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseCors("AllowAllOrigins");
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}