using Services.Models.Product.Base;

namespace Services.Models.Product
{
    public class ProductForView 
    {
        public IList<ProductForViewItems> Products { get; set; } = new List<ProductForViewItems>();

    }
    public class ProductForViewItems : ProductBaseDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
