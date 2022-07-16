using EnvironmentTests.Data;
using EnvironmentTests.Data.Entities;
using EnvironmentTests.DataAccess;
using EnvironmentTests.Interface;
using EnvironmentTests.Mapping;
using EnvironmentTests.Models.Response;
using EnvironmentTests.Models.Settings;
using EnvironmentTests.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EnvironmentTests
{
    public class Program
    {
        private const string ApiTitle = "Test API";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var appsettings = new ProductsAppSettings();
            builder.Configuration.Bind("ProductsApi", appsettings);
            var settings = new ProductsSettings(appsettings);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(a =>
            {
                a.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                a.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                a.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ProductsDbContext>(a =>
            {
                a.UseSqlServer(settings.DbConnectionString, b =>
                {
                    b.EnableRetryOnFailure();
                    b.MigrationsAssembly(typeof(ProductsDbContext).Assembly.FullName);
                });

            }, ServiceLifetime.Transient, ServiceLifetime.Singleton);

            builder.Services.AddSingleton<Func<IProductsDbContext>>(a => () => a.GetRequiredService<ProductsDbContext>());
            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<IProductDataAccess, ProductDataAccess>();
            builder.Services.AddSingleton<IColourService, ColourService>();
            builder.Services.AddSingleton<IColourDataAccess, ColourDataAccess>();
            builder.Services.AddSingleton<IMapper<ColourEntity, string>, ColourToStringMapper>();
            builder.Services.AddSingleton<IMapper<ProductEntity, ProductResponse>, ProductMapper>();
            builder.Services.AddSingleton<IMapper<ColourEntity, ColourResponse>, ColourMapper>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiTitle);
                c.DocumentTitle = ApiTitle;
                c.EnableDeepLinking();
            });

            app.MapControllers();

            app.Run();
        }
    }
}