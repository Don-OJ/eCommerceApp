namespace eCommerceApp.Application.DTO.Cart
{
    // Represents a cart processing request in the eCommerce application
    public class ProcessCart
    {
        // The unique identifier for the product
        public Guid ProductId { get; set; }

        // The quantity of the product to be processed
        public int Quantity { get; set; }
    }
}
