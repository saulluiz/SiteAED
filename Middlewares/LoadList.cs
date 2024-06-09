public class LoadListMiddleWare
{
    private readonly RequestDelegate _next;

    public LoadListMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        FileModifier.ReadFile(PeopleFiles.GetFile(1));
        Console.WriteLine("Lista carregada");
        
        await _next(httpContext);
    }
}