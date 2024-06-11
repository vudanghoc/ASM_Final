using AutoMapper;
using Domain.Entities;
using Services.Models.Product;

namespace Services.MapperProfiles.Converters
{
    public class ProductConverter : ITypeConverter<IEnumerable<Product>, IEnumerable<ProductForView>>
    {
        public IEnumerable<ProductForView> Convert(IEnumerable<Product> source, IEnumerable<ProductForView> destination, ResolutionContext context)
        {
            foreach (var product in destination)
            {
                foreach(var member in source.ToList())  
                {
                    product.Products.Add(context.Mapper.Map<ProductForViewItems>(member));
                }
                yield return product;
            }
        }
    }
}
