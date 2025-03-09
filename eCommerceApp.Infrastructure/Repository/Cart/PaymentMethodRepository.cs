using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interface.Cart;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Repository.Cart
{
    // Repository class for handling payment methods
    public class PaymentMethodRepository(AppDbContext context) : IPaymentMethod
    {
        // Asynchronously retrieves a list of available payment methods
        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethods()
        {
            // Fetches payment methods from the database without tracking changes
            return await context.PaymentMethods.AsNoTracking().ToListAsync();
        }
    }
}
