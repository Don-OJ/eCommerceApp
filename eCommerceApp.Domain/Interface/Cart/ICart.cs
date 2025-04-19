using eCommerceApp.Domain.Entities.Cart;

namespace eCommerceApp.Domain.Interface.Cart
{
    public interface ICart
    {
        /// <summary>
        /// Saves the checkout history for a collection of archive entries.
        /// </summary>
        /// <param name="checkouts">A collection of archive entries to be saved.</param>
        /// <returns>The number of records saved.</returns>
        Task<int> SaveCheckOutHistory(IEnumerable<Archive> checkouts);
        Task<IEnumerable<Archive>> GetAllCheckoutHistory();
    }
}
