using eCommerceApp.Application.Mapping;
using eCommerceApp.Application.Services.Implementation;
using eCommerceApp.Application.Services.Interface;
using eCommerceApp.Application.Validations.Authentication;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using eCommerceApp.Application.DTO.Identity;
using eCommerceApp.Application.Validations;
using eCommerceApp.Application.Services.Implementation.IdentityAuthentication;
using eCommerceApp.Application.Services.Interface.Authentication;
using eCommerceApp.Application.Services.Interface.Cart;

namespace eCommerceApp.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingConfig)); // Registering AutoMapper with the specified mapping configuration
            services.AddScoped<IProductService, ProductService>(); // Registering the product service with scoped lifetime
            services.AddScoped<ICategoryService, CategoryService>(); // Registering the category service with scoped lifetime

            services.AddFluentValidationAutoValidation(); // Adding FluentValidation automatic validation
            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>(); // Registering validators from the assembly containing CreateUserValidator

            services.AddScoped<IValidationService, ValidationService>(); // Registering the validation service with scoped lifetime

            services.AddScoped<IAuthenticationService, AuthenticationService>(); // Registering the authentication service with scoped lifetime

            services.AddScoped<ICartService, ICartService>(); // Registering the Cart service.

            services.AddScoped<IPaymentMethodService, IPaymentMethodService>(); // registeting the payment service.

            return services; // Returning the service collection
        }
    }
}
