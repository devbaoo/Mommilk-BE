using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
using Domain.Entities;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        // DI
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _categoryRepository = unitOfWork.Category;
        }

        public async Task<IActionResult> GetCategories(CategoryFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _categoryRepository.GetAll();

                if (filter.Name != null)
                {
                    query = query.Where(x => x.Name.Contains(filter.Name));
                }

                if (filter.AgeRange != null)
                {
                    query = query.Where(x => x.AgeRange.Contains(filter.AgeRange));
                }

                if (filter.TargetAudience != null)
                {
                    query = query.Where(x => x.TargetAudience.Contains(filter.TargetAudience));
                }

                if (filter.MilkType != null)
                {
                    query = query.Where(x => x.MilkType.Contains(filter.MilkType));
                }

                var totalRows = _categoryRepository.Count();
                var categories = await query
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                //return new OkObjectResult(categories.ToPaged(pagination, totalRows));
                return categories.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Lay by Id
        public async Task<IActionResult> GetCategory(Guid id)
        {
            try
            {
                var category = await _categoryRepository.Where(x => x.Id.Equals(id))
                    .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
                if (category == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }

                return category.Ok();
            }
            catch (Exception)
            {
                throw;
            }

        }

        // Tao Cate
        public async Task<IActionResult> CreateCategory(CategoryCreateModel model)
        {
            try
            {
                var category = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    TargetAudience = model.TargetAudience,
                    AgeRange = model.AgeRange,
                    MilkType = model.MilkType,
                    Icon = model.Icon,
                };
                _categoryRepository.Add(category);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    category.Ok();
                }

                return AppErrors.CREATE_FAIL.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Cap Nhat Cate
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryUpdateModel model)
        {
            try
            {
                var category = await _categoryRepository.Where(x => x.Id.Equals(id))
                    .FirstOrDefaultAsync();
                if (category == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }

                if (model.Name != null)
                {

                    category.Name = model.Name;
                }
                _categoryRepository.Update(category);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return await GetCategory(id);
                }
                return AppErrors.UPDATE_FAIL.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
