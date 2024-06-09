public class LoadListMiddleWare
{
    private readonly RequestDelegate _next;

    public LoadListMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        Console.WriteLine("Middleware");
        
        await _next(httpContext);
    }
}