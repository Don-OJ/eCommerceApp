using AutoMapper;
using eCommerceApp.Application.DTO.Cart;
using eCommerceApp.Application.DTO.Response;
using eCommerceApp.Application.Services.Interface.Cart;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interface;
using eCommerceApp.Domain.Interface.Cart;
using eCommerceApp.Domain.Interface.IdentityAuthentication;

namespace eCommerceApp.Application.Services.Implementation.Cart
{
    public class CartService(ICart cartInterface, IMapper mapper, IGeneric<Product> productInterface,
            IPaymentMethodService paymentMethodService, IPaymentService paymentService, IUserManagement userManagement) :
            ICartService
    {
        // Processes the checkout for the given user and cart items
        public async Task<ServiceResponse> CheckOut(CheckOut checkout)
        {
            // Calculate the total amount and get the products in the cart
            var (products, totalAmount) = await GetCartTotalAmount(checkout.Carts);

            // Retrieve available payment methods
            var paymentMethods = await paymentMethodService.GetPaymentMethods();

            // Check if the selected payment method is valid
            if (checkout.PaymentMethodId == paymentMethods.FirstOrDefault()!.Id)
            {
                // Process the payment
                return await paymentService.Pay(totalAmount, products, checkout.Carts);
            }
            else
            {
                // Return an error response if the payment method is invalid
                return new ServiceResponse(false, "Invalid payment method");
            }
        }

        public async Task<IEnumerable<GetArchive>> GetArchives()
        {
            var history = await cartInterface.GetAllCheckoutHistory();
            if (history == null) return [];
            var groupByCustormerId = history.GroupBy(x => x.UserId).ToList();
            var products = await productInterface.GetAllAsync();
            var archives = new List<GetArchive>();
            foreach (var customerId in groupByCustormerId)
            {
                var customerDetails = await userManagement.GetUserById(customerId.Key!);
                foreach(var item in customerId)
                {
                    var product = products.FirstOrDefault(x => x.Id == item.ProductId);
                    archives.Add(new GetArchive
                    {
                        ProductName = product!.Name,
                        QuantityOrdered = item.Quantity,
                        CustomerName = customerDetails.Fullname,
                        CustomerEmail = customerDetails.Email,
                        AmountPaid = item.Quantity * product.Price,
                        DatePurchased = item.DateCreated
                    });
                }
            }
            return archives;
        }

        // Saves the checkout history for the given archives
        public async Task<ServiceResponse> SaveCheckOutHistory(IEnumerable<CreateArchive> archives)
        {
            // Map the archives to the domain model
            var mappedData = mapper.Map<IEnumerable<Archive>>(archives);

            // Save the checkout history
            var result = await cartInterface.SaveCheckOutHistory(mappedData);

            // Return a success or failure response based on the result
            return result > 0 ? new ServiceResponse(true, "CheckOut history saved successfully") :
                new ServiceResponse(false, "Failed to save CheckOut history");
        }

        // Calculates the total amount for the given cart items
        private async Task<(IEnumerable<Product>, decimal)> GetCartTotalAmount(IEnumerable<ProcessCart> carts)
        {
            // Return zero if there are no cart items
            if (!carts.Any()) return ([], 0);

            // Retrieve all products
            var products = await productInterface.GetAllAsync();
            if (!products.Any()) return ([], 0);

            // Get the products in the cart
            var cartProducts = carts
                .Select(cartItem => products.FirstOrDefault(p => p.Id == cartItem.ProductId))
                .Where(product => product != null)
                .ToList();

            // Calculate the total amount for the cart items
            var totalAmount = carts
                .Where(cartItem => cartProducts.Any(p => p.Id == cartItem.ProductId))
                .Sum(cartItem => cartItem.Quantity * cartProducts.First(p => p.Id == cartItem.ProductId)!.Price);

            return (cartProducts!, totalAmount);
        }
    }
}
