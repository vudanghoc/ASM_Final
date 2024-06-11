using Domain.Entities;
using Services.Models.Order;

namespace Services.Contracts.Services
{
    public interface IOrderService
    {

        Task<OrderForView> GetAllOrders();
        Task<OrderForViewItems> GetOrderById(int id);
        /*Task<OrderForView> GetAllOrders();
        Task<bool> UpdateOrder(OrderForUpdate comboDto, int id);
        Task<Order> AddOrder(OrderForCreate comboDto);
        Task<bool> DeleteOrder(int id);*/
    }
}
