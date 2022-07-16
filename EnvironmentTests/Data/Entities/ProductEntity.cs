namespace EnvironmentTests.Data.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<ColourEntity> Colours { get; set; } = new HashSet<ColourEntity>();
    }
}
