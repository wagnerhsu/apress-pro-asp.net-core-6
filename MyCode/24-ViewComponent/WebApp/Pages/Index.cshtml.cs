using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Pages;

public class IndexModel : PageModel {
    private DataContext _context;

    public Product? Product { get; set; }

    public IndexModel(DataContext ctx) {
        _context = ctx;
    }

    public async Task<IActionResult> OnGetAsync(long id = 1) {
        Product = await _context.Products.FindAsync(id);
        if (Product == null) {
            return RedirectToPage("NotFound");
        }
        return Page();
    }
}
