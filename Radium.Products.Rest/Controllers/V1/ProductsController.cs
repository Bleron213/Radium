using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radium.Products.Application.Rest.Commands;
using Radium.Products.Application.Rest.Queries;
using Radium.Products.Entities.Models;
using Radium.Products.Rest.Contracts.Requests;
using Radium.Products.Rest.Contracts.Response;
using Radium.Shared.Utils.Responses;

namespace Radium.Products.Rest.Controllers.V1
{
    [ApiVersion("1.0")]
    public class ProductsController : ApiControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        [ProducesResponseType(400, Type = typeof(ErrorDetails))]
        [ProducesResponseType(401, Type = typeof(ErrorDetails))]
        [ProducesResponseType(500, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetProductById(int productId)
        {
            _logger.LogDebug("START: {method} (GET) - Paramters ({productId})", nameof(GetProductById), productId);

            var query = GetProductByIdQuery.Create(productId);
            var result = await Mediator.Send(query);

            _logger.LogDebug("END: {method} (GET) - Paramters ({productId}) - SUCCESS", nameof(GetProductById), productId);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductDto>))]
        [ProducesResponseType(400, Type = typeof(ErrorDetails))]
        [ProducesResponseType(401, Type = typeof(ErrorDetails))]
        [ProducesResponseType(500, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> GetProducts()
        {
            _logger.LogDebug("START: {method} (GET)", nameof(GetProducts));

            var query = GetProductsQuery.Create();
            var result = await Mediator.Send(query);

            _logger.LogDebug("END: {method} (GET) - SUCCESS", nameof(GetProducts));

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        [ProducesResponseType(400, Type = typeof(ErrorDetails))]
        [ProducesResponseType(401, Type = typeof(ErrorDetails))]
        [ProducesResponseType(500, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateModel productCreateModel)
        {
            _logger.LogDebug("START: {method} (POST) Body ({productCreateModel})", nameof(CreateProduct), productCreateModel);

            var command = CreateProductCommand.Create(productCreateModel);
            var result = await Mediator.Send(command);

            _logger.LogDebug("END: {method} (POST) Body ({productCreateModel}) - SUCCESS", nameof(CreateProduct), productCreateModel);

            return Ok(result);
        }

        [HttpPut("{productId}")]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        [ProducesResponseType(400, Type = typeof(ErrorDetails))]
        [ProducesResponseType(401, Type = typeof(ErrorDetails))]
        [ProducesResponseType(500, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductUpdateModel productUpdateModel)
        {
            _logger.LogDebug("START: {method} (PUT) Parameters ({productId}) Body ({productUpdateModel})", nameof(UpdateProduct), productId, productUpdateModel);

            var command = UpdateProductCommand.Create(productId, productUpdateModel);
            var result = await Mediator.Send(command);

            _logger.LogDebug("START: {method} (PUT) Parameters ({productId}) Body ({productUpdateModel}) - SUCCESS", nameof(UpdateProduct), productId, productUpdateModel);

            return Ok(result);
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorDetails))]
        [ProducesResponseType(401, Type = typeof(ErrorDetails))]
        [ProducesResponseType(500, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            _logger.LogDebug("START: {method} (DELETE) Parameters ({productId})", nameof(DeleteProduct), productId);

            var command = DeleteProductCommand.Create(productId);
            var result = await Mediator.Send(command);

            _logger.LogDebug("START: {method} (DELETE) Parameters ({productId}) - SUCCESS", nameof(DeleteProduct), productId);

            return Ok(result);
        }
    }
}
