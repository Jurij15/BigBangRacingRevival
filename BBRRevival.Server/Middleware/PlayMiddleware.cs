using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace BBRRevival.Server.Middleware
{
    public class PlayMiddleware
    {
        private readonly RequestDelegate _next;

        public PlayMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //intercept the response to add headers before theyre sent to the client
            context.Response.OnStarting(() =>
            {
                if (!context.Response.HasStarted)
                {
                    // TODO: actuall status here
                    context.Response.Headers.Add("PLAY_STATUS", "OK");
                }

                return Task.CompletedTask;
            });

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
