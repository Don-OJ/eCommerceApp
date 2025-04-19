using eCommerceApp.Application.DTO.Cart;
using eCommerceApp.Application.Services.Interface.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {
        [HttpPost("checkout")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CheckOut(CheckOut checkout)
        {
            // check model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await cartService.CheckOut(checkout);
            return result.Succcess ? Ok(result) : BadRequest(result);
        }
        [HttpPost("save-checkout")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> SaveCheckOutHistory(IEnumerable<CreateArchive> archives)
        {
            var result = await cartService.SaveCheckOutHistory(archives);
            return result.Succcess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("get-archives")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCheckoutHistory()
        {
            var archives = await cartService.GetArchives();
            return archives.Any() ? Ok(archives) : NotFound("No archives found");
        }
    }
}
