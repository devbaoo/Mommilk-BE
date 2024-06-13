using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Data;
using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Domain.Entities;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _productRepository = unitOfWork.Product;
        }
        // Get product
        public async Task<IActionResult> GetProducts(ProductFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _productRepository.GetAll();

                if (filter.Name != null)
                {
                    query = query.Where(x => x.Name.Contains(filter.Name));
                }

                if (filter.Origin != null)
                {
                    query = query.Where(x => x.Origin.Contains(filter.Origin));
                }

                if (filter.Ingredient != null)
                {
                    query = query.Where(x => x.Ingredient.Contains(filter.Ingredient));
                }

                if (filter.SweetLevel != null)
                {
                    query = query.Where(x => x.SweetLevel.Contains(filter.SweetLevel));
                }
                if (filter.Flavour != null)
                {
                    query = query.Where(x => x.Flavour.Contains(filter.Flavour));
                }
                if (filter.Sample != null)
                {
                    query = query.Where(x => x.Sample.Contains(filter.Sample));
                }
                if (filter.Capacity != null)
                {
                    query = query.Where(x => x.Capacity.Contains(filter.Capacity));
                }
                if (filter.Description != null)
                {
                    query = query.Where(x => x.Description.Contains(filter.Description));
                }
                //if (filter.Price != null)
                //{
                //    query = query.Where(x => x.Price.Contains(filter.Price));
                //}
                // if (filter.Quantity != null)
                // {
                //     query = query.Where(x => x.Quantity.Contains(filter.Quantity));
                // }
                //if (filter.Id != null)
                // {
                //     query = query.Where(x => x.Id.Contains(filter.Id));
                // }
                //  if (filter.StoreId != null)
                //  {
                //      query = query.Where(x => x.StoreId.Contains(filter.StoreId));
                // }
                // if (filter.CreateAt != null)
                // {
                //     query = query.Where(x => x.CreateAt.Contains(filter.CreateAt));
                //  }
                if (filter.Status != null)
                {
                    query = query.Where(x => x.Status.Contains(filter.Status));
                }

                var totalRows = _productRepository.Count();
                var products = await query

                    .Paginate(pagination)
                    .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                //return new OkObjectResult(categories.ToPaged(pagination, totalRows));
                return products.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Tao Product
        public async Task<IActionResult> CreateProduct(ProductCreateModel model)
        {
            try
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Origin = model.Origin,
                    Brand = model.Brand,
                    Ingredient = model.Ingredient,
                    SweetLevel = model.SweetLevel,
                    Flavour = model.Flavour,
                    Sample = model.Sample,
                    Capacity = model.Capacity,
                    Description = model.Description,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    ExpireAt = model.ExpireAt,
                    StoreId = model.StoreId,
                    Status = model.Status,

                   
                };
                _productRepository.Add(product);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    product.Ok();
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


