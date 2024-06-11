using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public bool IsHidden { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<ProductCombo>? ProductCombos { get; set;}
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
