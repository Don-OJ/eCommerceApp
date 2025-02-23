using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interface;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            }),
            ServiceLifetime.Scoped);

            services.AddScoped<IGeneric<Product>, GenericRepository<Product>>();
            services.AddScoped<IGeneric<Category>, GenericRepository<Category>>();

            return services;
        }
    }
}
