using eCommerceApp.Domain.Entities.Cart;

namespace eCommerceApp.Domain.Interface.Cart
{
    public interface IPaymentMethod
    {
        /// <summary>
        /// Retrieves a list of available payment methods.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains a collection of PaymentMethod objects.</returns>
        Task<IEnumerable<PaymentMethod>> GetPaymentMethods();
    }
}
