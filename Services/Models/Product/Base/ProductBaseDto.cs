namespace Services.Models.Product.Base
{
    public class ProductBaseDto 
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public bool IsHidden { get; set; }
        public int CategoryId { get; set; }
    }
}
