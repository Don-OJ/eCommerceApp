using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTO.Product
{
    public class UpdateProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}
