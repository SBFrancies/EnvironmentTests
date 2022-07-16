using EnvironmentTests.Models.Request;
using EnvironmentTests.Models.Response;

namespace EnvironmentTests.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetProductsAsync(string colour);
        Task<ProductResponse> GetProductAsync(int id);

        Task DeleteProductAsync(int id);

        Task<ProductResponse> CreateProductAsync(CreateProductRequest request);

        Task<ProductResponse> UpdateProductAsync(UpdateProductRequest request);
    }
}
