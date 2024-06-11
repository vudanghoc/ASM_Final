using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int OrderTotal { get; set; }
        public Status Status { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUser? User { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
