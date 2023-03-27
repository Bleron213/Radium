using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Radium.Products.Application.Rest.Queries;

namespace Radium.Products.Rest.Controllers.V1
{
    [ApiVersion("1.0")]
    public class ProductsController : ApiControllerBase
    {
        public ProductsController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var query = GetProductByIdQuery.Create(productId);
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
