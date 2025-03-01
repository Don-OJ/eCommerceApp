using eCommerceApp.Application.Services.Interface.Logging;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interface;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Middleware.Exception;
using eCommerceApp.Infrastructure.Repository;
using eCommerceApp.Infrastructure.Repository.Services;
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
            // register services in DI container
            string connectionString = "DevConnection";
            services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(config.GetConnectionString(connectionString),
            sqlOptions =>
            { // Ensure this is the ccorrect assembly
                sqlOptions.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure(); // enable automatic retry for transient failures.
            }).UseExceptionProcessor(),
            ServiceLifetime.Scoped);

            services.AddScoped<IGeneric<Product>, GenericRepository<Product>>();
            services.AddScoped<IGeneric<Category>, GenericRepository<Category>>();

            services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));

            services.AddDefaultIdentity<AppUser>(options => 
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;  
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredUniqueChars = 2;
                //options.User.RequireUniqueEmail = true;
                //options.Lockout.MaxFailedAccessAttempts = 3;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            
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

            return services;
        }

        // register the middleware in DI container
        public static IApplicationBuilder UseInfrastructureService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
