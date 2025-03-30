using Konecta.Core.Models;
using Konecta.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Konecta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // ✅ Admin & User can view
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll() =>
            Ok(await _productService.GetAllProductsAsync());

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // 🔒 Admin only can create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            var created = await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // 🔒 Admin only can update
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            product.Id = id;
            var result = await _productService.UpdateProductAsync(product);
            return result ? Ok() : NotFound();
        }

        //  Admin only can delete
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}
