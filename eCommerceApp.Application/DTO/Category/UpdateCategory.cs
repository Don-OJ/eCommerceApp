using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTO.Category
{
    public class UpdateCategory : CategoryBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}
