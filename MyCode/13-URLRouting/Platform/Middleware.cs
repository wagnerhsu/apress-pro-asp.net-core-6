

namespace Platform;

public class QueryStringMiddleWare
{
    private RequestDelegate? next;
    private readonly ILogger<QueryStringMiddleWare> _logger;

    public QueryStringMiddleWare()
    {
        // do nothing
    }

    public QueryStringMiddleWare(RequestDelegate nextDelegate,
        ILogger<QueryStringMiddleWare> logger)
    {
        next = nextDelegate;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogDebug("context.Request.Path: {Path}", context.Request.Path);
        if (context.Request.Method == HttpMethods.Get
            && context.Request.Query["custom"] == "true")
        {
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "text/plain";
            }

            await context.Response.WriteAsync("Class-based Middleware \n");
        }

        if (next != null)
        {
            await next(context);
        }
    }
}

public class LocationMiddleware
{
    private RequestDelegate next;
    private MessageOptions options;

    public LocationMiddleware(RequestDelegate nextDelegate,
        IOptions<MessageOptions> opts)
    {
        next = nextDelegate;
        options = opts.Value;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path == "/location")
        {
            await context.Response
                .WriteAsync($"{options.CityName}, {options.CountryName}");
        }
        else
        {
            await next(context);
        }
    }
}
