using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Norwind.Data;
using Norwind.DTO;
using Norwind.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Norwind.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly NorthwindContext _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(NorthwindContext context, ILogger<ProductController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _context.Products
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    SupplierId = p.SupplierId,
                    CategoryId = p.CategoryId,
                    Unit = p.Unit,
                    Price = p.Price
                })
                .ToListAsync();

            return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id)
    {
        var product = await _context.Products
            .Where(p => p.ProductId == id)
            .Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                SupplierId = p.SupplierId,
                CategoryId = p.CategoryId,
                Unit = p.Unit,
                Price = p.Price
            })
            .FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(ProductDTO productDTO)
    {
        var product = new Product
        {
            ProductName = productDTO.ProductName,
            SupplierId = productDTO.SupplierId,
            CategoryId = productDTO.CategoryId,
            Unit = productDTO.Unit,
            Price = productDTO.Price
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDTO productDTO)
    {
        var existingProduct = await _context.Products.FindAsync(id);

        if (existingProduct == null)
        {
            return NotFound();
        }

        existingProduct.ProductName = productDTO.ProductName;
        existingProduct.SupplierId = productDTO.SupplierId;
        existingProduct.CategoryId = productDTO.CategoryId;
        existingProduct.Unit = productDTO.Unit;
        existingProduct.Price = productDTO.Price;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}
