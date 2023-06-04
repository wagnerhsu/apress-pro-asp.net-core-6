using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Pages;

public class HandlerSelectorModel : PageModel
{
    private DataContext _context;
    private readonly ILogger<HandlerSelectorModel> _logger;

    public Product? Product { get; set; }

    public HandlerSelectorModel(DataContext ctx, ILogger<HandlerSelectorModel> logger)
    {
        _context = ctx;
        _logger = logger;
    }

    public async Task OnGetAsync(long id = 1)
    {
        Product = await _context.Products.FindAsync(id);
    }

    public async Task OnGetRelatedAsync(long id = 1)
    {
        Product = await _context.Products
            .Include(p => p.Supplier)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductId == id);
        if (Product != null && Product.Supplier != null)
        {
            Product.Supplier.Products = null;
        }
        if (Product != null && Product.Category != null)
        {
            Product.Category.Products = null;
        }
    }

    public async Task OnPostAsync()
    {
        _logger.LogInformation("OnPostAsync");
        await Task.CompletedTask;
    }

    public async Task OnPostTestAsync()
    {
        _logger.LogInformation("OnPostTestAsync");
        await Task.CompletedTask;
    }
}
