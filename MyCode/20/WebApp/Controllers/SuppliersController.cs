using System;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using WebApp.Models;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private DataContext context;

    public SuppliersController(DataContext ctx)
    {
        context = ctx;
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get supplier by id",
        Description = "id should be a number",
        OperationId = "GetSupplierId",
        Tags = new[] { "Suppliers" })]
    public async Task<Supplier?> GetSupplier([SwaggerParameter(Description = "id should be large than zero")] long id)
    {
        var supplier = await context.Suppliers
            .Include(s => s.Products)
            .FirstAsync(s => s.SupplierId == id);
        if (supplier != null)
        {
            foreach (var p in supplier.Products)
            {
                p.Supplier = null;
            }
        }
        return supplier;
    }

    [HttpPatch("{id}")]
    public async Task<Supplier?> PatchSupplier(long id, JsonPatchDocument<Supplier> patchDoc)
    {
        Supplier? s = await context.Suppliers.FindAsync(id);
        if (s != null)
        {
            patchDoc.ApplyTo(s);
            await context.SaveChangesAsync();
        }
        return s;
    }
}
