using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IActionResult> GetProducts(ProductFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> CreateProduct(ProductCreateModel model);
        Task<IActionResult> GetProduct(int id);
        Task<IActionResult> UpdateProduct(int id, ProductUpdateModel model);
    }
}
