using eCommerceApp.Application.DTO.Cart;
using eCommerceApp.Application.DTO.Response;

namespace eCommerceApp.Application.Services.Interface.Cart
{
    public interface ICartService
    {
        /// <summary>
        /// Saves the checkout history for a collection of checkout archives.
        /// </summary>
        /// <param name="checkouts">A collection of checkout archives to be saved.</param>
        /// <returns>A service response indicating the result of the operation.</returns>
        Task<ServiceResponse> SaveCheckOutHistory(IEnumerable<CreateArchive> checkouts);

        /// <summary>
        /// Processes the checkout for a user.
        /// </summary>
        /// <param name="checkout">The checkout details including payment method and carts.</param>
        /// <param name="userId">The unique identifier of the user performing the checkout.</param>
        /// <returns>A service response indicating the result of the operation.</returns>
        Task<ServiceResponse> CheckOut(CheckOut checkout);
        Task<IEnumerable<GetArchive>> GetArchives();
    }
}
