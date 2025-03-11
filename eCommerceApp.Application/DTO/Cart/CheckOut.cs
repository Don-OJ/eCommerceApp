using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTO.Cart
{
    // Represents the checkout process in the eCommerce application
    public class CheckOut
    {
        // The unique identifier for the selected payment method
        [Required]
        public required Guid PaymentMethodId { get; set; }

        // A collection of carts to be processed during checkout
        [Required]
        public required IEnumerable<ProcessCart> Carts { get; set; }
    }
}
