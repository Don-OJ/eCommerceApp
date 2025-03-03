using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Data
{
    // DbContext for the application, inheriting from IdentityDbContext to include identity management
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
    {
        // DbSet representing the Products table in the database
        public DbSet<Product> Products { get; set; }

        // DbSet representing the Categories table in the database
        public DbSet<Category> Categories { get; set; }

        // DbSet representing the RefreshTokens table in the database
        public DbSet<RefreshToken> RefreshToken { get; set; }
    }
}
