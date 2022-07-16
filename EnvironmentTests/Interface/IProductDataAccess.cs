using EnvironmentTests.Data.Entities;

namespace EnvironmentTests.Interface
{
    public interface IProductDataAccess
    {
        Task<IEnumerable<ProductEntity>> GetProductsAsync(string colour);

        Task<ProductEntity> GetProductAsync(int id);

        Task<ProductEntity> UpdateProductAsync(ProductEntity entity, ICollection<string> colours);

        Task<ProductEntity> CreateProductAsync(ProductEntity entity, ICollection<string> colours);

        Task DeleteProductAsync(int id);
    }
}
