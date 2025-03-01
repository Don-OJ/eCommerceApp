using AutoMapper;
using eCommerceApp.Application.DTO.Product;
using eCommerceApp.Application.DTO.Response;
using eCommerceApp.Application.Services.Interface;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interface;

namespace eCommerceApp.Application.Services.Implementation
{
    public class ProductService(IGeneric<Product> productInterFace, IMapper mapper) : IProductService
    {
        public async Task<ServiceResponse> AddAsync(CreateProduct product)
        {
            var mappedData = mapper.Map<Product>(product);
            int result = await productInterFace.AddAsync(mappedData);

            return result > 0 ? new ServiceResponse(true, "Product Added!") :
                new ServiceResponse(false, "Product NOT Added!");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await productInterFace.DeleteAsync(id);
            if(result == 0) return new ServiceResponse(false, "Product NOT Deleted!");

            return result > 0 ? new ServiceResponse(true, "Product Deleted!") : 
                new ServiceResponse(false, "Product NOT Deleted!");
            
        }

        public async Task<IEnumerable<GetProduct>> GetAllAsync()
        {
            var rawData =  await productInterFace.GetAllAsync();
            if (rawData.Count() == 0) return [];

            return mapper.Map<IEnumerable<GetProduct>>(rawData);
        }

        public async Task<GetProduct> GetByIdAsync(Guid id)
        {
            var rawData = await productInterFace.GetByIdAsync(id);
            if (rawData==null) return new GetProduct();

            return mapper.Map<GetProduct>(rawData);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProduct product)
        {
            var mappedData = mapper.Map<Product>(product);
            int result = await productInterFace.UpdateAsync(mappedData);

            return result > 0 ? new ServiceResponse(true, "Product Updated!") :
                new ServiceResponse(false, "Product NOT Updated!");
        }
    }
}
