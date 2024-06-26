
namespace RestaurantAPI2._0.Middlewares
{
    public interface IExceptionLoggerMiddleware
    {
        Task InvokeAsync(HttpContext context, RequestDelegate next);
    }
}