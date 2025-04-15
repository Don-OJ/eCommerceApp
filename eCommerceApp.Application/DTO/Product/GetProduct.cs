using eCommerceApp.Application.DTO.Category;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTO.Product
{
    public class GetProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
        public GetCategory? Category { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
