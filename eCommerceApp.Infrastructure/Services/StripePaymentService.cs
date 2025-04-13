using eCommerceApp.Application.DTO.Cart;
using eCommerceApp.Application.DTO.Response;
using eCommerceApp.Application.Services.Interface.Cart;
using eCommerceApp.Domain.Entities;
using Stripe.Checkout;

namespace eCommerceApp.Infrastructure.Services
{
    public class StripePaymentService : IPaymentService
    {
        public async Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> cartProducts, IEnumerable<ProcessCart> carts)
        {
            try
            {
                // Create line items for the payment session
                var lineItems = new List<SessionLineItemOptions>();
                foreach (var item in cartProducts)
                {
                    // Find the quantity of the current product in the cart
                    var productQuantity = carts.FirstOrDefault(x => x.ProductId == item.Id);

                    // Add the product details to the line items
                    lineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Name,
                                Description = item.Description
                            },
                            UnitAmount = (long)item.Price * 100, // Convert price to cents
                        },
                        Quantity = productQuantity!.Quantity, // Set the quantity of the product
                    });
                }

                // Create payment session options
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" }, // Specify payment method types
                    LineItems = lineItems, // Set the line items
                    Mode = "payment", // Set the mode to payment
                    SuccessUrl = "https://localhost:7004/payment-success", // URL to redirect to on successful payment
                    CancelUrl = "https://localhost:7004/payment-cancel" // URL to redirect to on canceled payment
                };

                // Create a new session service
                var service = new SessionService();
                // Create the payment session asynchronously
                Session session = await service.CreateAsync(options);

                // Return a successful service response with the session URL
                return new ServiceResponse(true, session.Url);
            }
            catch (Exception ex)
            {
                // Return a failed service response with the exception message
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
