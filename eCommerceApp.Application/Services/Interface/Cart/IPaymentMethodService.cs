using eCommerceApp.Application.DTO.Cart;

namespace eCommerceApp.Application.Services.Interface.Cart
{
    public interface IPaymentMethodService
    {
        // Asynchronously retrieves the payment method details.
        // Returns a GetPaymentMethod object containing the payment method information.
        Task<IEnumerable<GetPaymentMethod>> GetPaymentMethods();
    }
}
