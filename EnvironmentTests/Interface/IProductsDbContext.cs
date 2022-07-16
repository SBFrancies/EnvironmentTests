using EnvironmentTests.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EnvironmentTests.Interface
{
    public interface IProductsDbContext
    {
        DbSet<ColourEntity> Colours { get; }

        DbSet<ProductEntity> Products { get; }

        Task<bool> CanConnectAsync();

        ValueTask DisposeAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}