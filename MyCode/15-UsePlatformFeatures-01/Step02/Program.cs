using PlatformCommon;
using Serilog;
using Serilog.WxLibrary;
using Step02;

var serilogService = new SerilogService(SerilogService.DefaultOptions);
serilogService.Initialize();
var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
    logging.AddSerilog();
});
builder.Services.Configure<MessageOptions>(builder.Configuration.GetSection("Location"));
var app = builder.Build();

app.Map("/", EndPoints.Home);
app.Run();
