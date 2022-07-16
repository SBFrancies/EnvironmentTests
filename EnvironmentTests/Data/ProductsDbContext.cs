using EnvironmentTests.Data.Entities;
using EnvironmentTests.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EnvironmentTests.Data
{
    public class ProductsDbContext : DbContext, IProductsDbContext
    {
        private const string Schema = "dbo";
        public DbSet<ColourEntity> Colours { get; set; }

        public DbSet<ProductEntity> Products { get; set; }

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {

        }

        public async Task<bool> CanConnectAsync()
        {
            return await Database.CanConnectAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.ToTable("Products", Schema, a => a.IsTemporal(
            b =>
            {
                b.HasPeriodStart("ValidFrom");
                b.HasPeriodEnd("ValidTo");
                b.UseHistoryTable("ProductsHistory", Schema);
            }));

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.HasMany(e => e.Colours)
                .WithMany(e => e.Products)
                .UsingEntity("ProductColours", a => a.ToTable("ProductColours", Schema, b => b.IsTemporal(
            c =>
            {
                c.HasPeriodStart("ValidFrom");
                c.HasPeriodEnd("ValidTo");
                c.UseHistoryTable("ProductColoursHistory", Schema);
            })));
            });

            modelBuilder.Entity<ColourEntity>(entity =>
            {
                entity.ToTable("Colours", Schema, a => a.IsTemporal(
            b =>
            {
                b.HasPeriodStart("ValidFrom");
                b.HasPeriodEnd("ValidTo");
                b.UseHistoryTable("ColoursHistory", Schema);
            }));
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.HasIndex(e => e.Colour).IsUnique();
                entity.Property(e => e.Colour).HasMaxLength(100).IsRequired();
            });
        }
    }
}
