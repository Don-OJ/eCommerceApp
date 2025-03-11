using AutoMapper;
using eCommerceApp.Application.DTO.Category;
using eCommerceApp.Application.DTO.Product;
using eCommerceApp.Application.DTO.Response;
using eCommerceApp.Application.Services.Interface;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interface;
using eCommerceApp.Domain.Interface.Category;

namespace eCommerceApp.Application.Services.Implementation
{
    public class CategoryService(IGeneric<Category> productInterFace, IMapper mapper,
        ICategory categorySpecific) : ICategoryService
    {
        public async Task<ServiceResponse> AddAsync(CreateCategory product)
        {
            var mappedData = mapper.Map<Category>(product);
            int result = await productInterFace.AddAsync(mappedData);

            return result > 0 ? new ServiceResponse(true, "Category Added!") :
                new ServiceResponse(false, "Category NOT Added!");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await productInterFace.DeleteAsync(id);
            if (result == 0) return new ServiceResponse(false, "Category NOT Deleted!");

            return result > 0 ? new ServiceResponse(true, "Category Deleted!") :
                new ServiceResponse(false, "Category NOT Deleted!");
        }

        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var rawData = await productInterFace.GetAllAsync();
            if (rawData.Count() == 0) return [];

            return mapper.Map<IEnumerable<GetCategory>>(rawData);
        }

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var rawData = await productInterFace.GetByIdAsync(id);
            if (rawData == null) return new GetCategory();

            return mapper.Map<GetCategory>(rawData);
        }

        public async Task<IEnumerable<GetProduct>> GetProductsByCategory(Guid categoryId)
        {
            var products = await categorySpecific.GetProductByCategory(categoryId);

            if(!products.Any()) 
                return [];

            return mapper.Map<IEnumerable<GetProduct>>(products);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory product)
        {
            var mappedData = mapper.Map<Category>(product);
            int result = await productInterFace.UpdateAsync(mappedData);

            return result > 0 ? new ServiceResponse(true, "Category Updated!") :
                new ServiceResponse(false, "Category NOT Updated!");
        }
    }
}
