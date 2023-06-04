using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase {
    private DataContext _context;

    public SuppliersController(DataContext ctx) {
        _context = ctx;
    }

    [HttpGet("{id}")]
    public async Task<Supplier?> GetSupplier(long id) {
        Supplier supplier = await _context.Suppliers.Include(s => s.Products)
            .FirstAsync(s => s.SupplierId == id);
        if (supplier.Products != null) {
            foreach (Product p in supplier.Products) {
                p.Supplier = null;
            };
        }
        return supplier;
    }

    [HttpPatch("{id}")]
    public async Task<Supplier?> PatchSupplier(long id,
        JsonPatchDocument<Supplier> patchDoc) {
        Supplier? s = await _context.Suppliers.FindAsync(id);
        if (s != null) {
            patchDoc.ApplyTo(s);
            await _context.SaveChangesAsync();
        }
        return s;
    }
}
