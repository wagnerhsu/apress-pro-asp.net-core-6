using System.Text.Json.WxLibrary.Extensions;
using Microsoft.AspNetCore.HostFiltering;
using Serilog;
using Serilog.WxLibrary;

var serilogService = new SerilogService(SerilogService.DefaultOptions);
serilogService.Initialize();
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Logging.AddSerilog();

builder.Services.Configure<HostFilteringOptions>(opts =>
{
    opts.AllowedHosts.Clear();
    opts.AllowedHosts.Add("*.example.com");
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opts =>
{
    opts.IdleTimeout = TimeSpan.FromMinutes(30);
    opts.Cookie.IsEssential = true;
    Serilog.Log.Logger.Debug(opts.ToJson(true));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error.html");
    app.UseStaticFiles();
}

app.UseSession();

app.UseStatusCodePages("text/html", Platform.Responses.DefaultResponse);

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/error")
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await Task.CompletedTask;
    }
    else
    {
        await next();
    }
});

app.MapGet("/session", async context =>
{
    int counter1 = (context.Session.GetInt32("counter1") ?? 0) + 1;
    int counter2 = (context.Session.GetInt32("counter2") ?? 0) + 1;
    context.Session.SetInt32("counter1", counter1);
    context.Session.SetInt32("counter2", counter2);
    await context.Session.CommitAsync();
    await context.Response
        .WriteAsync($"Counter1: {counter1}, Counter2: {counter2}");
});
app.Run(context =>
{
    throw new Exception("Something has gone wrong");
});

app.Run();
