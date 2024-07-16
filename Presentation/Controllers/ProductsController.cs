using Application.Services.Interfaces;
using Domain.Constants;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("filter")]
        public async Task<IActionResult> GetProducts([FromBody] ProductFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _productService.GetProducts(filter, pagination);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {
            try
            {
                return await _productService.GetProduct(id);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPost]
        [Authorize(UserRoles.STAFF)]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateModel model)
        {
            try
            {
                return await _productService.CreateProduct(model);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPut]
        [Authorize(UserRoles.STAFF)]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromForm] ProductUpdateModel model)
        {
            try
            {
                return await _productService.UpdateProduct(id, model);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
