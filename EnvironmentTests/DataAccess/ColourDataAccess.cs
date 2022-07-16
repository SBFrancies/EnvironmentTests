using EnvironmentTests.Data.Entities;
using EnvironmentTests.Interface;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentTests.DataAccess
{
    public class ColourDataAccess : IColourDataAccess
    {
        private Func<IProductsDbContext> DbContextFactory { get; }

        public ColourDataAccess(Func<IProductsDbContext> dbContextFactory)
        {
            DbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        public async Task<IEnumerable<ColourEntity>> GetColoursAsync()
        {
            await using var context = DbContextFactory();

            return await context.Colours.ToListAsync();
        }

        public async Task<ColourEntity> GetColourAsync(int id)
        {
            await using var context = DbContextFactory();

            return await context.Colours.FirstOrDefaultAsync(a => a.Id == id);
        }
        
        public async Task<ColourEntity> UpdateColourAsync(ColourEntity entity)
        {
            await using var context = DbContextFactory();

            context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<ColourEntity> CreateColourAsync(ColourEntity entity)
        {
            await using var context = DbContextFactory();

            context.Colours.Add(entity);

            await context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteColourAsync(int id)
        {
            await using var context = DbContextFactory();

            var colour = await context.Colours.FirstOrDefaultAsync(a => a.Id == id);

            context.Colours.Remove(colour);

            await context.SaveChangesAsync();
        }
    }
}
