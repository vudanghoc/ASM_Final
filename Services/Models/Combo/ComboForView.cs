using Services.Models.Combo.Base;

namespace Services.Models.Combo
{
    public class ComboForView
    {
        public IList<ComboForViewItems> Combos { get; set; } = new List<ComboForViewItems>();
    }
    public class ComboForViewItems : ComboBaseDto
    {
        public int Id { get; set; }
        public List<ProductComboInfoForView> ProductCombos { get; set; } = new List<ProductComboInfoForView>();

    }
    public class ProductComboInfoForView
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
