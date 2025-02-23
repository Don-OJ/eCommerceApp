using AutoMapper;
using eCommerceApp.Application.DTO.Category;
using eCommerceApp.Application.DTO.Product;
using eCommerceApp.Domain.Entities;

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
        }
    }
}
