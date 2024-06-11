using DataAccess.DataAccess.Base;
using Domain.Entities;
using Persistence;
using Services.Contracts.DataAccess;

namespace DataAccess.DataAccess
{
    public class OrderDetailDataAccess: GeneralDataAccess<OrderDetail>, IOrderDetailDataAccess
    {
        public OrderDetailDataAccess(ApplicationDbContext context) : base(context)
        {
            
        }
        Task<OrderDetail> IOrderDetailDataAccess.AddOrderDetail(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }

        bool IOrderDetailDataAccess.DeleteOrderDetail(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<OrderDetail>> IOrderDetailDataAccess.GetAllOrderDetails()
        {
            throw new NotImplementedException();
        }

        Task<OrderDetail> IOrderDetailDataAccess.GetOrderDetailById(int id)
        {
            throw new NotImplementedException();
        }

        OrderDetail IOrderDetailDataAccess.UpdateOrderDetail(OrderDetail orderDetail)
        {
            return Update(orderDetail);
        }
    }
}
