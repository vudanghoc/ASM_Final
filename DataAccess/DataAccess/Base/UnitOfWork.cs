using Persistence;
using Services.Contracts.DataAccess;
using Services.Contracts.DataAccess.Base;

namespace DataAccess.DataAccess.Base
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IOrderDataAccess OrderDataAccess { get; }
        public IOrderDetailDataAccess OrderDetailDataAccess { get; }
        public IProductDataAccess ProductDataAccess { get; }
        public UnitOfWork(ApplicationDbContext context,
            IOrderDataAccess orderDataAccess,
            IOrderDetailDataAccess orderDetailDataAccess,
            IProductDataAccess productDataAccess)
        {
            _context = context;
            OrderDataAccess = orderDataAccess;
            ProductDataAccess = productDataAccess;
            //OrderDetailDataAccess = orderDetailDataAccess;
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
