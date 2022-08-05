// Copyright (c) xxx, 2022. All rights reserved.

using System.Text.Json.WxLibrary.Extensions;
using Microsoft.Extensions.Options;
using PlatformCommon;

namespace Step02;

internal static class EndPoints
{
    public static async Task Home(HttpContext context, IOptions<MessageOptions> options, ILogger<Program> logger)
    {
        logger.LogInformation("Hello World");

        await context.Response.WriteAsync("Hello World!" + options.Value.ToJson(true));
    }
}
