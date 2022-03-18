// Copyright (c) xxx, 2022. All rights reserved.

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ContentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("string")]
        public string GetString() => "This is a string response";

        [HttpGet("object")]
        public async Task<Product> GetObject()
        {
            return await _dataContext.Products.FirstAsync();
        }
    }
}
