using Serilog;
using Serilog.WxLibrary;

var serilogService = new SerilogService(SerilogService.DefaultOptions);
serilogService.Initialize();
var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
    logging.AddSerilog();
});
var app = builder.Build();

app.Map("/", async context =>
{
    var loggerFactory = context.RequestServices.GetService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogInformation("Hello World");
    await context.Response.WriteAsync("Hello World!");
});
app.Run();
