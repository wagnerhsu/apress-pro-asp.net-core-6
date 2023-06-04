using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase {
    private DataContext _context;

    public ProductsController(DataContext ctx) {
        _context = ctx;
    }

    [HttpGet]
    public IAsyncEnumerable<Product> GetProducts() {
        return _context.Products.AsAsyncEnumerable();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(long id) {
        Product? p = await _context.Products.FindAsync(id);
        if (p == null) {
            return NotFound();
        }
        return Ok(p);
    }

    [HttpPost]
    public async Task<IActionResult>
        SaveProduct(ProductBindingTarget target) {
        Product p = target.ToProduct();
        await _context.Products.AddAsync(p);
        await _context.SaveChangesAsync();
        return Ok(p);
    }

    [HttpPut]
    public async Task UpdateProduct(Product product) {
        _context.Update(product);
        await _context.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task DeleteProduct(long id) {
        _context.Products.Remove(new Product() { ProductId = id });
        await _context.SaveChangesAsync();
    }

    [HttpGet("redirect")]
    public IActionResult Redirect() {
        return RedirectToAction(nameof(GetProduct), new { Id = 1 });
    }
}
