using eCommerceApp.Application.DTO.Cart;
using eCommerceApp.Application.DTO.Response;

namespace eCommerceApp.Application.Services.Interface.Cart
{
    public interface ICartService
    {
       Task<ServiceResponse> SaveCheckOutHistory(IEnumerable<CreateArchive> checkouts);
       Task<ServiceResponse> CheckOut(IEnumerable<CreateArchive> checkouts);
    }
}
