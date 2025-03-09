using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interface.Cart;
using eCommerceApp.Infrastructure.Data;

namespace eCommerceApp.Infrastructure.Repository.Cart
{
    public class CartRepository(AppDbContext context) : ICart
    {
        public async Task<int> SaveCheckOutHistory(IEnumerable<Archive> checkouts)
        {
            context.CheckOutArchives.AddRange(checkouts);
            return await context.SaveChangesAsync();
        }
    }
}
