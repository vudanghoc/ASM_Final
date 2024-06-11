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
        public async Task<Order> AddOrder(Order order)
        {
            await _context.AddAsync(order);
            return order;
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

        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders
                .Include(od => od.OrderDetails)
                .ThenInclude(p => p.Product)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public Order UpdateOrder(Order order)
        {
            _context.Update(order);
            return order;
            //return Update(order);
        }
    }
}
