using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interface.Category;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Repository.Category
{
    public class CategoryRepository(AppDbContext context) : ICategory
    {
        public async Task<IEnumerable<Product>> GetProductByCategory(Guid categoryId)
        {
            var products = await context
                .Products
                .Include(x => x.Category)
                .Where(x => x.CategoryId == categoryId)
                .AsNoTracking()
                .ToListAsync();

            return products.Count > 0 ? products : [];
        }
    }
}
