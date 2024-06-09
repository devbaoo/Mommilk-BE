using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] CategoryFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _categoryService.GetCategories(filter, pagination);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] Guid id)
        {
            try
            {
                return await _categoryService.GetCategory(id);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateModel model)
        {
            try
            {
                return await _categoryService.CreateCategory(model);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] CategoryUpdateModel model)
        {
            try
            {
                return await _categoryService.UpdateCategory(id, model);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
