using Domain.Entities;
using Services.Contracts.DataAccess.Base;

namespace Services.Contracts.DataAccess
{
    public interface IOrderDetailDataAccess: IGeneralDataAcces<OrderDetail>
    {
        Task<OrderDetail> AddOrderDetail(OrderDetail orderDetail);
        bool DeleteOrderDetail(OrderDetail orderDetail);
        Task<IEnumerable<OrderDetail>> GetAllOrderDetails();
        Task<OrderDetail> GetOrderDetailById(int id);
        OrderDetail UpdateOrderDetail(OrderDetail orderDetail);
    }
}
