using Services.Models.Order.Base;

namespace Services.Models.Order
{
    public class OrderForView
    {
        public IList<OrderForViewItems> Orders { get; set; } = new List<OrderForViewItems>();
    }

    public class OrderForViewItems: OrderBaseDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public IList<OrderDetailForView> OrderDetails { get; set; } = new List<OrderDetailForView>();

    }
    public class OrderDetailForView
    {
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        //public decimal Price { get; set; }
    }
}
