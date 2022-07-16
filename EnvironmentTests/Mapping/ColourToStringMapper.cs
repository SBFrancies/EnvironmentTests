using EnvironmentTests.Data.Entities;

namespace EnvironmentTests.Mapping
{
    public class ColourToStringMapper : BaseMapper<ColourEntity, string>
    {
        public override string Map(ColourEntity item)
        {
            if (item == null)
            {
                return null;
            }

            return item.Colour;
        }
    }
}
