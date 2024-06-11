using Services.Models.Combo.Base;

namespace Services.Models.Combo
{
    public class ComboForUpdate :ComboBaseDto
    {
        public List<ProductComboInfoForUpdate> ProductCombos { get; set; } = new List<ProductComboInfoForUpdate>();
    }
    public class ProductComboInfoForUpdate
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
