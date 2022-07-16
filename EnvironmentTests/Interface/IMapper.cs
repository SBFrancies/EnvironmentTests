namespace EnvironmentTests.Interface
{
    public interface IMapper<TFrom, TTo>
    {
        TTo Map(TFrom item);

        IEnumerable<TTo> Map(IEnumerable<TFrom> items);
    }
}
