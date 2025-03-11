using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interface.Cart;
using eCommerceApp.Infrastructure.Data;

namespace eCommerceApp.Infrastructure.Repository.Cart
{
    // Repository class for handling cart-related database operations
    public class CartRepository(AppDbContext context) : ICart
    {
        // Method to save checkout history to the database
        public async Task<int> SaveCheckOutHistory(IEnumerable<Archive> checkouts)
        {
            // Add the collection of checkout archives to the database context
            context.CheckOutArchives.AddRange(checkouts);
            // Save changes asynchronously and return the number of affected rows
            return await context.SaveChangesAsync();
        }
    }
}
