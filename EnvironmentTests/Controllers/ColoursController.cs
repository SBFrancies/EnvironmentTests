using EnvironmentTests.Interface;
using EnvironmentTests.Models.Request;
using EnvironmentTests.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EnvironmentTests.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ColoursController : ControllerBase
    {
        private IColourService ColourService { get; }
        private ILogger<ColoursController> Logger { get; }

        public ColoursController(IColourService colourService, ILogger<ColoursController> logger)
        {
            ColourService = colourService ?? throw new ArgumentNullException(nameof(colourService));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ColourResponse[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await ColourService.GetColoursAsync();

                if (result == null || !result.Any())
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled exception occured whilst fetching Colours");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error has occured");
            }
        }


        [HttpGet]
        [Route("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ColourResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            try
            {
                var result = await ColourService.GetColourAsync(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled exception occured whilst fetching Colour");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error has occured");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                await ColourService.DeleteColourAsync(id);

                return NoContent();
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled exception occured whilst deleting Colour");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error has occured");
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ColourResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] UpdateColourRequest request)
        {
            try
            {
                var result = await ColourService.UpdateColourAsync(request);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled exception occured whilst updating Colour");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error has occured");
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ColourResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] CreateColourRequest request)
        {
            try
            {
                var result = await ColourService.CreateColourAsync(request);

                if (result == null)
                {
                    return NotFound();
                }

                return StatusCode((int)HttpStatusCode.Created, result);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled exception occured whilst creating Colour");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error has occured");
            }
        }
    }
}
