using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Domain.Entities.Cart
{
    public class PaymentMethod
    {
        // Primary key for the PaymentMethod entity
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Name of the payment method (e.g., Credit Card, PayPal)
        public string Name { get; set; } = string.Empty;
    }
}
