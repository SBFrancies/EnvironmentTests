using EnvironmentTests.Interface;
using EnvironmentTests.Models.Request;
using EnvironmentTests.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EnvironmentTests.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService ProductService { get; }
        private ILogger<ProductsController> Logger { get; }

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            ProductService = productService ?? throw new ArgumentNullException(nameof(productService));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductResponse[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery]string colour)
        {
            try
            {
                var result = await ProductService.GetProductsAsync(colour);

                if (result == null || !result.Any())
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled exception occured whilst fetching Products");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error has occured");
            }
        }


        [HttpGet]
        [Route("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            try
            {
                var result = await ProductService.GetProductAsync(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled exception occured whilst fetching Product");
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
                await ProductService.DeleteProductAsync(id);

                return NoContent();
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled exception occured whilst deleting Product");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error has occured");
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] UpdateProductRequest request)
        {
            try
            {
                var result = await ProductService.UpdateProductAsync(request);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled exception occured whilst updating Product");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error has occured");
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] CreateProductRequest request)
        {
            try
            {
                var result = await ProductService.CreateProductAsync(request);

                if (result == null)
                {
                    return NotFound();
                }

                return StatusCode((int)HttpStatusCode.Created, result);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Unhandled exception occured whilst creating Product");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error has occured");
            }
        }
    }
}
