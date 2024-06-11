using Services.Models.Order.Base;

namespace Services.Models.Order
{
    public class OrderForCreate : OrderBaseDto
    {
        public IList<OrderDetailForCreate> OrderDetails { get; set; } = new List<OrderDetailForCreate>();
    }
    public class OrderDetailForCreate
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
