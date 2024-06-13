using Application.Services.Implementations;
using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/ProductCate")]
    [ApiController]
    public class ProductCatesController : ControllerBase
    {
        private readonly IProductCateService _productCateService;
        public ProductCatesController(IProductCateService productCateService)
        {
            _productCateService = productCateService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductCate([FromQuery] ProductCateFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _productCateService.GetProductCate(filter, pagination);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductCate([FromBody] ProductCateCreateModel model)
        {
            try
            {
                return await _productCateService.CreateProductCate(model);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
