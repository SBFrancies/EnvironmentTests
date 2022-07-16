using EnvironmentTests.Data.Entities;
using EnvironmentTests.Interface;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentTests.DataAccess
{
    public class ProductDataAccess : IProductDataAccess
    {
        private Func<IProductsDbContext> DbContextFactory { get; }

        public ProductDataAccess(Func<IProductsDbContext> dbContextFactory)
        {
            DbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        public async Task<IEnumerable<ProductEntity>> GetProductsAsync(string colour)
        {
            await using var context = DbContextFactory();

            var products = context.Products.Include(a => a.Colours).AsQueryable();

            if (!string.IsNullOrEmpty(colour))
            {
                products = products.Where(a => a.Colours.Any(b => b.Colour == colour));
            }

            var productsList = await products.ToListAsync();

            return productsList;
        }

        public async Task<ProductEntity> GetProductAsync(int id)
        {
            await using var context = DbContextFactory();

            return await context.Products.Include(a => a.Colours).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<ProductEntity> UpdateProductAsync(ProductEntity entity, ICollection<string> colours)
        {
            await using var context = DbContextFactory();

            var colourList = new List<ColourEntity>();

            foreach (var colour in colours)
            {
                var colourEntity = await context.Colours.FirstOrDefaultAsync(a => a.Colour == colour);

                if (colourEntity == null)
                {
                    colourEntity = new ColourEntity
                    {
                        Colour = colour,
                    };

                    entity.Colours.Add(colourEntity);
                }
            }

            context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return entity;

        }

        public async Task<ProductEntity> CreateProductAsync(ProductEntity entity, ICollection<string> colours)
        {
            await using var context = DbContextFactory();

            var colourList = new List<ColourEntity>();

            foreach(var colour in colours)
            {
                var colourEntity = await context.Colours.FirstOrDefaultAsync(a => a.Colour == colour);

                if(colourEntity == null)
                {
                    colourEntity = new ColourEntity
                    {
                        Colour = colour,
                    };
                }

                entity.Colours.Add(colourEntity);
            }

            context.Products.Add(entity);

            await context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteProductAsync(int id)
        {
            await using var context = DbContextFactory();

            var product = await context.Products.FirstOrDefaultAsync(a => a.Id == id);

            context.Products.Remove(product);

            await context.SaveChangesAsync();
        }
    }
}
