namespace EnvironmentTests.Data.Entities
{
    public class ColourEntity
    {
        public int Id { get; set; }

        public string Colour { get; set; }

        public ICollection<ProductEntity> Products { get; set; } = new HashSet<ProductEntity>();
    }
}
