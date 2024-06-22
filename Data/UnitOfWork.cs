﻿using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SuaMe88Context _context;
        private IDbContextTransaction _transaction = null!;

        public UnitOfWork(SuaMe88Context context)
        {
            _context = context;
        }

        // Getter
        public ICategoryRepository _category = null!;
        //public ICategoryRepository _category = null!;
        public IOrderRepository _order = null!;
        public IProductRepository _product = null!;
        public IOrderDetailRepository _orderDetail = null!;
        public IFeedbackRepository _feedback = null!;
        public IProductCategoryRepository _productCategory = null!;
        public IOrderTransactionRepository _orderTransaction = null!;

        //Setter
        public ICategoryRepository Category
        {
            get { return _category ??= new CategoryRepository(_context); }
        }
        public IOrderRepository Order
        {
            get { return _order ??= new OrderRepository(_context); }
        }
        public IProductRepository Product
        {
            get { return _product ??= new ProductRepository(_context); }
        }
        public IOrderDetailRepository OrderDetail
        {
            get { return _orderDetail ??= new OrderDetailRepository(_context); }
        }
        public IOrderTransactionRepository OrderTransaction
        {
            get { return _orderTransaction ??= new OrderTransactionRepository(_context); }
        }
        public IProductCategoryRepository ProductCategory
        {
            get { return _productCategory ??= new ProductCategoryRepository(_context); }
        }

        public IFeedbackRepository Feedback
        {
            get { return _feedback ??= new FeedbackRepository(_context); }
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null!;
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null!;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
