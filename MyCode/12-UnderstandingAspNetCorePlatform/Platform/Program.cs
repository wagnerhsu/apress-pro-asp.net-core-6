var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MessageOptions>(options =>
{
    options.CityName = "Albany";
});

var app = builder.Build();
IApplicationBuilder appBuilder = app;
appBuilder.Use(async (httpContext, next) =>
{
    await httpContext.Response.WriteAsync("Hi， \n");
    await next(httpContext);
});

app.Use(async (context, next) =>
{
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync("Middleware01\n");
    await next();
});
app.UseMiddleware<LocationMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
