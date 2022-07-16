using EnvironmentTests.Models.Request;
using EnvironmentTests.Models.Response;

namespace EnvironmentTests.Interface
{
    public interface IColourService
    {
        Task<IEnumerable<ColourResponse>> GetColoursAsync();

        Task<ColourResponse> GetColourAsync(int id);

        Task DeleteColourAsync(int id);


        Task<ColourResponse> CreateColourAsync(CreateColourRequest request);

        Task<ColourResponse> UpdateColourAsync(UpdateColourRequest request);
    }
}
