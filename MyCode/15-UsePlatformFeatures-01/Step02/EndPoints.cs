// Copyright (c) xxx, 2022. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Step02
{
    internal static class EndPoints
    {
        public static async Task Home(HttpContext context, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation("Hello World");
            await context.Response.WriteAsync("Hello World!");
        }
    }
}
