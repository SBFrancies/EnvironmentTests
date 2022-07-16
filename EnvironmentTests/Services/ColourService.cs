using EnvironmentTests.Data.Entities;
using EnvironmentTests.Interface;
using EnvironmentTests.Models.Request;
using EnvironmentTests.Models.Response;

namespace EnvironmentTests.Services
{
    public class ColourService : IColourService
    {
        private IColourDataAccess ColourDataAccess { get; }

        private ILogger<ColourService> Logger { get; }

        private IMapper<ColourEntity, ColourResponse> ColourMapper { get; }

        public ColourService(
            IColourDataAccess colourDataAccess,
            ILogger<ColourService> logger,
            IMapper<ColourEntity, ColourResponse> colourMapper)
        {
            ColourDataAccess = colourDataAccess ?? throw new ArgumentNullException(nameof(colourDataAccess));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ColourMapper = colourMapper ?? throw new ArgumentNullException(nameof(colourMapper));
        }

        public async Task<IEnumerable<ColourResponse>> GetColoursAsync()
        {
            Logger.LogInformation("Retrieving Colour list");

            var colours = await ColourDataAccess.GetColoursAsync();

            var response = ColourMapper.Map(colours);

            return response;
        }

        public async Task<ColourResponse> GetColourAsync(int id)
        {
            Logger.LogInformation("Retrieving Colour");

            var colour = await ColourDataAccess.GetColourAsync(id);

            var response = ColourMapper.Map(colour);

            return response;
        }

        public async Task DeleteColourAsync(int id)
        {
            Logger.LogInformation("Deleting Colour");

            await ColourDataAccess.DeleteColourAsync(id);
        }

        public async Task<ColourResponse> CreateColourAsync(CreateColourRequest request)
        {
            Logger.LogInformation("Creating Colour");

            var colour = new ColourEntity
            {
                Colour = request.Colour,
            };

            colour = await ColourDataAccess.CreateColourAsync(colour);

            var response = ColourMapper.Map(colour);

            return response;
        }

        public async Task<ColourResponse> UpdateColourAsync(UpdateColourRequest request)
        {
            Logger.LogInformation("Updating Colour");

            var colour = new ColourEntity
            {
               Id = request.Id,
               Colour = request.Colour,
            };

            colour = await ColourDataAccess.UpdateColourAsync(colour);

            var response = ColourMapper.Map(colour);

            return response;
        }
    }
}
