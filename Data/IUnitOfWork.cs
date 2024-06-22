﻿using Data.Repositories.Interfaces;

namespace Data
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get; }
        public IOrderRepository Order { get; }
        public IProductRepository Product { get; }
        public IOrderDetailRepository OrderDetail { get; }
        public IProductCategoryRepository ProductCategory { get; }
        public IFeedbackRepository Feedback { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
        void Dispose();
        Task<int> SaveChangesAsync();
    }
}
