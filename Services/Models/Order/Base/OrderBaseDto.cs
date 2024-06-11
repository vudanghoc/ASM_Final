using Domain.Enum;

namespace Services.Models.Order.Base
{
    public class OrderBaseDto
    {
        public string UserId { get; set; }
        public Status Status { get; set; }
        public int OrderTotal { get; set; }
    }
}
