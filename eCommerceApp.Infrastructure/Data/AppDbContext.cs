using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
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

        // Dbset representing the payment methods table in the database
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        // DbSet representing the CartItems table in the database
        public DbSet<Archive> CheckOutArchives { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // seed payemnt methods
            builder.Entity<PaymentMethod>().HasData(
                new PaymentMethod
                {
                    Id = Guid.NewGuid(),
                    Name = "Credit Card"
                },
                new PaymentMethod
                {
                    Id = Guid.NewGuid(),
                    Name = "PayPal"
                },
                new PaymentMethod
                {
                    Id = Guid.NewGuid(),
                    Name = "Cash"
                });

            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "NAME"
                });
        }
    }
}
