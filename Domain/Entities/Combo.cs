using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Combo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Promotion { get; set; }
        public ICollection<ProductCombo>? ProductCombos { get; set; }
    }
}
