using DataAccess.DataAccess.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace DataAccess.DataAccess
{
    public class OrderDataAccess : GeneralDataAccess<Order>, IOrderDataAccess
    {
        public OrderDataAccess(ApplicationDbContext context) : base(context)
        {
        }
        public Task<Order> AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders
                .Include(od => od.OrderDetails)
                .ThenInclude(p => p.Product)
                .ToListAsync();
        }

        public Task<Order> GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public Order UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
