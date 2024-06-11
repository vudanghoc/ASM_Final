using Domain.Entities;
using Services.Models.Order;

namespace Services.Contracts.Services
{
    public interface IOrderService
    {
        Task<OrderForView> GetAllOrders();
        Task<OrderForViewItems> GetOrderById(int id);
        Task<bool> UpdateOrderStatus(OrderForUpdateStatus orderDto, int id);
        Task<bool> UpdateOrder(OrderForUpdate orderDto, int id);
        Task<bool> AddOrder(OrderForCreate orderDto);

        /*Task<OrderForView> GetAllOrders();
        Task<bool> UpdateOrder(OrderForUpdate comboDto, int id);
        Task<bool> DeleteOrder(int id);*/
    }
}
