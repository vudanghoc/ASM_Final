using Services.Models.Category.Base;

namespace Services.Models.Category
{
    public class CategoryForView
    {
        public IList<CategoryForViewItems> Categories { get; set; } = new List<CategoryForViewItems>();
    }
    public class CategoryForViewItems : CategoryBaseDto
    {
        public int Id { get; set; }
    }
}
