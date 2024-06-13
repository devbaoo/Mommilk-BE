using Application.Services.Interfaces;
using AutoMapper;
using Common.Errors;
using Data.Repositories.Interfaces;
using Data;
using Domain.Entities;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;
using Data.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations
{
    public class ProductCateService : BaseService, IProductCateService
    {
        private readonly IProductCateRepository _productCateRepository;
        public ProductCateService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _productCateRepository = unitOfWork.ProductCate;
        }
        // Get productCate
        public async Task<IActionResult> GetProductCate(ProductCateFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _productCateRepository.GetAll();
            
                var totalRows = _productCateRepository.Count();
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

        // Tao ProductCate
        public async Task<IActionResult> CreateProductCate(ProductCateCreateModel model)
        {
            try
            {
                var productCate = new ProductCategory
                {
                    Id = Guid.NewGuid(),
                    CategoryId = Guid.NewGuid(),
                    ProductId = Guid.NewGuid(),
                   

                };
                _productCateRepository.Add(productCate);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    productCate.Ok();
                }

                return AppErrors.CREATE_FAIL.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    

    }
}
