using Application.Services.Interfaces;
using AutoMapper;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services.Implementations
{
    public class ProductCategoryService : BaseService, IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _productCategoryRepository = unitOfWork.ProductCategory;
        }
        // Get productCate
        public async Task<IActionResult> GetProductCate(ProductCategoryFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _productCategoryRepository.GetAll();

                var totalRows = _productCategoryRepository.Count();
                var products = await query

                    .Paginate(pagination)
                    // .ProjectTo<ProductCateViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                //return new OkObjectResult(categories.ToPaged(pagination, totalRows));
                return products.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
