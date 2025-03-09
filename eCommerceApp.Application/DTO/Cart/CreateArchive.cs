using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTO.Cart
{
    public class CreateArchive
    {
        // Identifier for the user associated with the archive entry
        [Required]
        public Guid UserId { get; set; }

        // Identifier for the product associated with the archive entry
        [Required]
        public Guid ProductId { get; set; }

        // Quantity of the product in the archive entry
        [Required]  
        public int Quantity { get; set; }
    }
}
