using Services.Models.Combo.Base;

namespace Services.Models.Combo
{
    public class ProductComboInfoForCreate
    {
        public int ProductId { get; set; }
        public int Quantity{ get; set; }
    }
    public class ComboForCreate : ComboBaseDto
    {
        public List<ProductComboInfoForCreate> ProductCombos { get; set; } = new List<ProductComboInfoForCreate>();
    }
}
