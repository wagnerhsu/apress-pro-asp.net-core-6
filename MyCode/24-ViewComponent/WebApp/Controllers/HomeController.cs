using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

public class HomeController : Controller {
    private DataContext _context;

    public HomeController(DataContext ctx) {
        _context = ctx;
    }

    public async Task<IActionResult> Index(long id = 1) {
        ViewBag.AveragePrice =
            await _context.Products.AverageAsync(p => p.Price);
        return View(await _context.Products.FindAsync(id));
    }

    public IActionResult List() {
        return View(_context.Products);
    }

    public IActionResult Html() {
        return View((object)"This is a <h3><i>string</i></h3>");
    }
}
