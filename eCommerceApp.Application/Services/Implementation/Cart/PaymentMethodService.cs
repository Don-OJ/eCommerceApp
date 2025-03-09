using AutoMapper;
using eCommerceApp.Application.DTO.Cart;
using eCommerceApp.Application.Services.Interface.Cart;
using eCommerceApp.Domain.Interface.Cart;

namespace eCommerceApp.Application.Services.Implementation.Cart
{
    // Implementation of the PaymentMethodService class which handles payment method related operations
    public class PaymentMethodService(IPaymentMethod paymentMethod, IMapper mapper)
        : IPaymentMethodService
    {
        // Asynchronously retrieves a list of payment methods
        public async Task<IEnumerable<GetPaymentMethod>> GetPaymentMethods()
        {
            // Fetch the payment methods from the data source
            var methods = await paymentMethod.GetPaymentMethods();

            // Check if there are no payment methods available
            if (!methods.Any())
            {
                // Return an empty list if no payment methods are found
                return [];
            }

            // Map the domain payment methods to DTOs and return them
            return mapper.Map<IEnumerable<GetPaymentMethod>>(methods);
        }
    }
}
