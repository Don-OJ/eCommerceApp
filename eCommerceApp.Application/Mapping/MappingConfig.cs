using AutoMapper;
using eCommerceApp.Application.DTO.Cart;
using eCommerceApp.Application.DTO.Category;
using eCommerceApp.Application.DTO.Identity;
using eCommerceApp.Application.DTO.Product;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Entities.Identity;

namespace eCommerceApp.Application.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Mapping configuration for creating a new category
            CreateMap<CreateCategory, Category>();

            // Mapping configuration for creating a new product
            CreateMap<CreateProduct, Product>();

            // Mapping configuration for retrieving category details
            CreateMap<Category, GetCategory>();

            // Mapping configuration for retrieving product details
            CreateMap<Product, GetProduct>();

            // Mapping configuration for creating a new user
            CreateMap<CreateUser, AppUser>();

            // Mapping configuration for logging in a user
            CreateMap<LoginUser, AppUser>();

            // Mapping configuration for retrieving payment method details
            CreateMap<PaymentMethod, GetPaymentMethod>();
        }
    }
}
