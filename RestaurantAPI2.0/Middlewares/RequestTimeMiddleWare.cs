
using System.Diagnostics;

namespace RestaurantAPI2._0.Middlewares
{
    public class RequestTimeMiddleWare : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleWare> logger;
        private Stopwatch _stopwatch;
        public RequestTimeMiddleWare(ILogger<RequestTimeMiddleWare> logger)
        {
            _stopwatch = new Stopwatch();
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Start();
            await next.Invoke(context);
            _stopwatch.Stop();

            var elapsedMiliseconds = _stopwatch.ElapsedMilliseconds;
            if(elapsedMiliseconds/1000 > 4) {
                var message = $"Request [{context.Request.Method}] at {context.Request.Path}] took {elapsedMiliseconds} ms";
                logger.LogInformation(message);
            }
        }
    }
}
