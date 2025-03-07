using AutoMapper;
using eCommerceApp.Application.DTO.Category;
using eCommerceApp.Application.DTO.Identity;
using eCommerceApp.Application.DTO.Product;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Identity;

namespace eCommerceApp.Application.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CreateCategory, Category>();
            CreateMap<CreateProduct, Product>();
            
            CreateMap<Category, GetCategory>();
            CreateMap<Product, GetProduct>();

            CreateMap<CreateUser, AppUser>();
            CreateMap<LoginUser, AppUser>();
        }
    }
}
