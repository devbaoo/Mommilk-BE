using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Common.Helpers;
using Data;
using Data.Repositories.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Implementations
{
    public class VoucherService(IUnitOfWork unitOfWork, IMapper mapper)
        : BaseService(unitOfWork, mapper), IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository = unitOfWork.Voucher;

        public async Task<IActionResult> GetVouchers(VoucherFilterModel filter, PaginationRequestModel pagination)
        {
            var query = _voucherRepository.GetAll();
            if(filter.Status != null && !filter.Status.IsNullOrEmpty())
            {
                query = query.Where(v => v.Status == filter.Status);
            }
            if (filter.Search != null && !filter.Search.IsNullOrEmpty()) { 
                query = query.Where(v => v.Code.Equals(filter.Search));
            }
            if (filter.MinOrderValue != null)
            {
                query = query.Where(v => v.MinOrderValue >= filter.MinOrderValue);
            }
            if (filter.From != null) 
            { 
                query = query.Where(v => v.From >= filter.From);
            }
            if (filter.To != null)
            {
                query = query.Where(v => v.To <= filter.To);
            }
            var totalRows = query.Count();
            var result = await query
                .ProjectTo<VoucherViewModel>(_mapper.ConfigurationProvider)
                .Paginate(pagination)
                .ToListAsync();
            if(result.Count > 0)
            {
                return result.ToPaged(pagination, totalRows).Ok();
            }
            return AppErrors.VOUCHER_NOT_EXIST.NotFound();
        }

        public async Task<IActionResult> GetValidVouchers()
        {
            var vouchers = await _voucherRepository
                .Where(v => v.Status.Equals(VoucherStatuses.ACTIVE) && v.To > DateTimeHelper.VnNow && v.Quantity > 0)
                .ProjectTo<VoucherViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return vouchers.Ok();
        }

        public async Task<IActionResult> GetVoucher(Guid id)
        {
            var voucher = await _voucherRepository
                .Where(v => v.Id == id)
                .ProjectTo<VoucherViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            if (voucher != null) 
            {
                return voucher.Ok();
            }
            return AppErrors.VOUCHER_NOT_EXIST.NotFound();
        }

        public async Task<IActionResult> CreateVoucher(VoucherCreateModel model)
        {
            var check = await ValidateNewVoucher(model);
            if (check is ObjectResult objectResult && objectResult.StatusCode != 200) {
                return check;
            }
            var voucher = _mapper.Map<Voucher>(model);
            _voucherRepository.Add(voucher);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                return await GetVoucher(voucher.Id);
            }
            return AppErrors.CREATE_FAIL.UnprocessableEntity();
        }

        public async Task<IActionResult> UpdateVoucher(Guid id, VoucherUpdateModel model)
        {
            var target = await _voucherRepository
                .Where(v => v.Id.Equals(id))
                .FirstOrDefaultAsync();

            if (target == null)
            {
                return AppErrors.VOUCHER_NOT_ENOUGH.NotFound();
            }
            if (await _voucherRepository
                    .Where(v => v.Code.Equals(model.Code))
                    .AnyAsync() && target.Code != model.Code)
            {
                return AppErrors.VOUCHER_DUPLICATE.UnprocessableEntity();
            }
            if (model.Value < 1)
            {
                return AppErrors.VOUCHER_NO_VALUE.UnprocessableEntity();
            }

            _mapper.Map(model, target);
            _voucherRepository.Update(target);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                return await GetVoucher(id);
            }
            return AppErrors.UPDATE_FAIL.UnprocessableEntity();
        }

        private async Task<IActionResult> ValidateNewVoucher(VoucherCreateModel model)
        {
            if (model.From >= model.To || model.From < DateTimeHelper.VnNow)
            {
                return AppErrors.INVALID_DATE.UnprocessableEntity();
            }
            if (await _voucherRepository
                    .Where(v => v.Code.Equals(model.Code))
                    .AnyAsync())
            {
                return AppErrors.VOUCHER_DUPLICATE.UnprocessableEntity();
            }
            if (model.Value < 1)
            {
                return AppErrors.VOUCHER_NO_VALUE.UnprocessableEntity();
            }
            return "Valid".Ok();
        }
    }
}
