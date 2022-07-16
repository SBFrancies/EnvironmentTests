using EnvironmentTests.Data.Entities;

namespace EnvironmentTests.Interface
{
    public interface IColourDataAccess
    {
        Task<IEnumerable<ColourEntity>> GetColoursAsync();

        Task<ColourEntity> GetColourAsync(int id);

        Task<ColourEntity> UpdateColourAsync(ColourEntity entity);

        Task<ColourEntity> CreateColourAsync(ColourEntity entity);

        Task DeleteColourAsync(int id);
    }
}
