using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTO.Category
{
    public class CategoryBase
    {
        [Required]
        public string? Name { get; set; }
    }
}
