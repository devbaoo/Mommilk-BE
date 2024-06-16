using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/ProductCategory")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryService _productCateService;
        public ProductCategoriesController(IProductCategoryService productCateService)
        {
            _productCateService = productCateService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductCate([FromQuery] ProductCategoryFilterModel filter, [FromQuery] PaginationRequestModel pagination)
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
    }
}
