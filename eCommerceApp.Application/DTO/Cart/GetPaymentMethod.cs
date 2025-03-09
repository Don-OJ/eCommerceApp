namespace eCommerceApp.Application.DTO.Cart
{
    public class GetPaymentMethod
    {
        // Primary key for the PaymentMethod entity
        public required Guid Id { get; set; }

        // Name of the payment method (e.g., Credit Card, PayPal)
        public required string Name { get; set; }
    }
}
