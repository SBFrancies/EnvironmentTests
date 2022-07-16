using EnvironmentTests.Interface;

namespace EnvironmentTests.Mapping
{
    public abstract class BaseMapper<TFrom, TTo> : IMapper<TFrom, TTo>
    {
        public abstract TTo Map(TFrom item);

        public IEnumerable<TTo> Map(IEnumerable<TFrom> items)
        {
            return items?.Select(item => Map(item));
        }
    }
}
