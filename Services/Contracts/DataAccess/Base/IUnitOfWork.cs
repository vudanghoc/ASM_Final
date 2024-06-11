using DataAccess.DataAccess;

namespace Services.Contracts.DataAccess.Base
{
    public interface IUnitOfWork
    {
        IOrderDataAccess OrderDataAccess { get; }
        IOrderDetailDataAccess OrderDetailDataAccess { get; }
        IProductDataAccess ProductDataAccess { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
