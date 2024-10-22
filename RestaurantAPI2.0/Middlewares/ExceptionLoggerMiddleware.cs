
using Microsoft.AspNetCore.Http.HttpResults;
using RestaurantAPI2._0.Exceptions;

namespace RestaurantAPI2._0.Middlewares
{
    public class ExceptionLoggerMiddleware : IMiddleware, IExceptionLoggerMiddleware
    {
        private readonly ILogger<ExceptionLoggerMiddleware> _logger;

        public ExceptionLoggerMiddleware(ILogger<ExceptionLoggerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFountException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (BadCredentialsException badRequest) {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequest.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
