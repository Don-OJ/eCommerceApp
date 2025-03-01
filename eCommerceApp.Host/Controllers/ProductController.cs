using eCommerceApp.Application.DTO.Product;
using eCommerceApp.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await productService.AddAsync(product);

            return result.Succcess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await productService.GetAllAsync();

            return data.Any() ? Ok(data) : NotFound(data);    
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetSingle(Guid Id)
        {
            var data = await productService.GetByIdAsync(Id);

            return data != null ? Ok(data) : NotFound() ;
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await productService.UpdateAsync(product);
            return result.Succcess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await productService.DeleteAsync(Id);
            return result.Succcess ? Ok(result) : BadRequest(result);
        }
    }
}
