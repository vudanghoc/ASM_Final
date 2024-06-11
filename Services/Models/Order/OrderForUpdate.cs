using Services.Models.Order.Base;

namespace Services.Models.Order
{
    public class OrderForUpdate: OrderBaseDto
    {
        public IList<OrderDetailForUpdate> OrderDetails { get; set; } = new List<OrderDetailForUpdate>();
    }
    public class OrderDetailForUpdate
    {
        //public int OrderDetailId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
