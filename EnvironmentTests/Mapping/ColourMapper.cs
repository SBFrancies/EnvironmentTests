using EnvironmentTests.Data.Entities;
using EnvironmentTests.Models.Response;

namespace EnvironmentTests.Mapping
{
    public class ColourMapper : BaseMapper<ColourEntity, ColourResponse>
    {
        public override ColourResponse Map(ColourEntity item)
        {
            if (item == null)
            {
                return null;
            }

            return new ColourResponse
            {
                Colour = item.Colour,
                Id = item.Id,
            };
        }
    }
}
