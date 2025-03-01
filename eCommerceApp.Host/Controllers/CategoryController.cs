﻿using eCommerceApp.Application.DTO.Category;
using eCommerceApp.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateCategory category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await categoryService.AddAsync(category);
            return result.Succcess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await categoryService.GetAllAsync();
            return data.Any() ? Ok(data) : NotFound(data);
        }
        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetSingle(Guid Id)
        {
            var data = await categoryService.GetByIdAsync(Id);
            return data != null ? Ok(data) : NotFound();
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateCategory category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await categoryService.UpdateAsync(category);
            return result.Succcess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await categoryService.DeleteAsync(Id);
            return result.Succcess ? Ok(result) : BadRequest(result);
        }
    }
}
