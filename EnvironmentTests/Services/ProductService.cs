using EnvironmentTests.Data.Entities;
using EnvironmentTests.Interface;
using EnvironmentTests.Models.Request;
using EnvironmentTests.Models.Response;

namespace EnvironmentTests.Services
{
    public class ProductService : IProductService
    {
        private IProductDataAccess ProductDataAccess { get; }

        private ILogger<ProductService> Logger { get; }

        private IMapper<ProductEntity, ProductResponse> ProductMapper { get; }

        public ProductService(
            IProductDataAccess productDataAccess,
            ILogger<ProductService> logger,
            IMapper<ProductEntity, ProductResponse> productMapper)
        {
            ProductDataAccess = productDataAccess ?? throw new ArgumentNullException(nameof(productDataAccess));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ProductMapper = productMapper ?? throw new ArgumentNullException(nameof(productMapper));
        }

        public async Task<IEnumerable<ProductResponse>> GetProductsAsync(string colour)
        {
            Logger.LogInformation("Retrieving Product list");

            var products = await ProductDataAccess.GetProductsAsync(colour);

            var response = ProductMapper.Map(products);

            return response;
        }

        public async Task<ProductResponse> GetProductAsync(int id)
        {
            Logger.LogInformation("Retrieving Product");

            var product = await ProductDataAccess.GetProductAsync(id);

            var response = ProductMapper.Map(product);

            return response;
        }

        public async Task DeleteProductAsync(int id)
        {
            Logger.LogInformation("Deleting Product");

            await ProductDataAccess.DeleteProductAsync(id);
        }

        public async Task<ProductResponse> CreateProductAsync(CreateProductRequest request)
        {
            Logger.LogInformation("Creating Product");

            var product = new ProductEntity
            {
                Description = request.Description,
                Name = request.Name,
            };

            product = await ProductDataAccess.CreateProductAsync(product, request.Colours);

            var response = ProductMapper.Map(product);

            return response;
        }

        public async Task<ProductResponse> UpdateProductAsync(UpdateProductRequest request)
        {
            Logger.LogInformation("Updating Product");

            var product = new ProductEntity
            {
                Id = request.Id,
                Description = request.Description,
                Name = request.Name,
            };

            product = await ProductDataAccess.UpdateProductAsync(product, request.Colours);

            var response = ProductMapper.Map(product);

            return response;
        }
    }
}
