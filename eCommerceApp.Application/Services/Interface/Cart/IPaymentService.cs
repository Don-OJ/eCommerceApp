using eCommerceApp.Application.DTO.Cart;
using eCommerceApp.Application.DTO.Response;
using eCommerceApp.Domain.Entities;

namespace eCommerceApp.Application.Services.Interface.Cart
{
    public interface IPaymentService
    {
        /// <summary>
        /// Processes the payment for the given cart products and total amount.
        /// </summary>
        /// <param name="totalAmount">The total amount to be paid.</param>
        /// <param name="cartProducts">The list of products in the cart.</param>
        /// <param name="carts">The list of cart processing requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a ServiceResponse object.</returns>
        Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> cartProducts,
            IEnumerable<ProcessCart> carts);
    }
}
