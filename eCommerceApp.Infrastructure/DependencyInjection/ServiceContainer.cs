using eCommerceApp.Application.Services.Interface.Cart;
using eCommerceApp.Application.Services.Interface.Logging;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interface;
using eCommerceApp.Domain.Interface.Cart;
using eCommerceApp.Domain.Interface.Category;
using eCommerceApp.Domain.Interface.IdentityAuthentication;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Middleware.Exception;
using eCommerceApp.Infrastructure.Repository;
using eCommerceApp.Infrastructure.Repository.Cart;
using eCommerceApp.Infrastructure.Repository.Category;
using eCommerceApp.Infrastructure.Repository.IdentityAuthentication;
using eCommerceApp.Infrastructure.Services;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace eCommerceApp.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService
            (this IServiceCollection services, IConfiguration config)
        {
            // Register the database context with SQL Server and exception processor
            string connectionString = "DevConnection";
            services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(config.GetConnectionString(connectionString),
            sqlOptions =>
            {
                // Ensure this is the correct assembly for migrations
                sqlOptions.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName);
                // Enable automatic retry for transient failures
                sqlOptions.EnableRetryOnFailure();
            }).UseExceptionProcessor(),
            ServiceLifetime.Scoped);

            // Register generic repositories for Product and Category entities
            services.AddScoped<IGeneric<Product>, GenericRepository<Product>>();
            services.AddScoped<IGeneric<Category>, GenericRepository<Category>>();

            // Register logging service
            services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));

            // Configure identity options and add default identity services
            services.AddDefaultIdentity<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredUniqueChars = 1;
                // Uncomment to enforce unique email and account lockout settings
                //options.User.RequireUniqueEmail = true;
                //options.Lockout.MaxFailedAccessAttempts = 3;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            // Configure JWT authentication
            services.AddAuthentication(options =>
            {
                // Set the default scheme for authentication
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // Set the default scheme for all authentication operations
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                // Set the default scheme for challenge operations
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    // Validate the audience of the token
                    ValidateAudience = true,
                    // Validate the issuer of the token
                    ValidateIssuer = true,
                    // Validate the lifetime of the token
                    ValidateLifetime = true,
                    // Require the token to have an expiration time
                    RequireExpirationTime = true,
                    // Validate the signing key of the token
                    ValidateIssuerSigningKey = true,
                    // Set the valid issuer for the token
                    ValidIssuer = config["JWT: Issuer"],
                    // Set the valid audience for the token
                    ValidAudience = config["JWT:Audience"],
                    // Set the clock skew to zero to prevent token expiration issues
                    ClockSkew = TimeSpan.Zero,
                    // Set the signing key for the token validation
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!))
                };
            });

            // Register user, role, and token management services
            services.AddScoped<IUserManagement, UserManagement>();
            services.AddScoped<IRoleManagement, RoleManagement>();
            services.AddScoped<ITokenManagement, TokenManagement>();
            services.AddScoped<IPaymentMethod, PaymentMethodRepository>(); // register payment method service
            services.AddScoped<IPaymentService, StripePaymentService>(); // register payment service
            services.AddScoped<ICart, CartRepository>();
            services.AddScoped<ICategory, CategoryRepository>(); // register category repository
            // register and configure stripe 
            Stripe.StripeConfiguration.ApiKey = config["Stripe:SecretKey"];


            return services;
        }

        // Register the middleware in DI container
        public static IApplicationBuilder UseInfrastructureService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
