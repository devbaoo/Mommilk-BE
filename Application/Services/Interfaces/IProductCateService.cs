using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IProductCateService
    {
        Task<IActionResult> GetProductCate(ProductCateFilterModel filter, PaginationRequestModel pagination);

        Task<IActionResult> CreateProductCate(ProductCateCreateModel model);
    }
}
