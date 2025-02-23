using eCommerceApp.Application.DTO.Product;

namespace eCommerceApp.Application.DTO.Category
{
    public class GetCategory : CategoryBase
    {
        public Guid Id { get; set; }
        public ICollection<GetProduct>? Products { get; set; }
    }
}