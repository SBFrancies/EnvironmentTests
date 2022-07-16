using EnvironmentTests.Data.Entities;
using EnvironmentTests.Interface;
using EnvironmentTests.Models.Response;

namespace EnvironmentTests.Mapping
{
    public class ProductMapper : BaseMapper<ProductEntity, ProductResponse>
    {
        private IMapper<ColourEntity, string> ColourMapper { get; }

        public ProductMapper(IMapper<ColourEntity, string> colourMapper)
        {
            ColourMapper = colourMapper ?? throw new ArgumentNullException(nameof(colourMapper));
        }

        public override ProductResponse Map(ProductEntity item)
        {
            if (item == null)
            {
                return null;
            }

            return new ProductResponse
            {
                Colours = ColourMapper.Map(item.Colours)?.ToArray(),
                Description = item.Description,
                Id = item.Id,
                Name = item.Name,
            };
        }
    }
}
