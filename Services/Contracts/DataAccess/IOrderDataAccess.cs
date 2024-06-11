using Domain.Entities;
using Services.Contracts.DataAccess.Base;

namespace DataAccess.DataAccess
{
    public interface IOrderDataAccess: IGeneralDataAcces<Order>
    {
        Task<Order> AddOrder(Order order);
        bool DeleteOrder(Order order);
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Order UpdateOrder(Order order);
    }
}