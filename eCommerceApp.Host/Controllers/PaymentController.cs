using eCommerceApp.Application.DTO.Cart;
using eCommerceApp.Application.Services.Interface.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentMethodService paymentMethodService) : ControllerBase
    {
        // Endpoint to retrieve all available payment methods
        [HttpGet("methods")]
        public async Task<ActionResult<IEnumerable<GetPaymentMethod>>> GetPaymentMethods()
        {
            // Fetch payment methods from the service
            var methods = await paymentMethodService.GetPaymentMethods();

            // Check if any payment methods were found
            if (!methods.Any())
            {
                // Return 404 Not Found if no payment methods are available
                return NotFound();
            }
            else
            {
                // Return 200 OK with the list of payment methods
                return Ok(methods);
            }
        }
    }
}
