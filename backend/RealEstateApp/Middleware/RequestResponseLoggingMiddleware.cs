using Serilog;

namespace RealEstateApp.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Log.Information("Handling request: {Method} {Url}",
                context.Request.Method, context.Request.Path);

            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            Log.Information("Response: {StatusCode} {ResponseBody}",
                context.Response.StatusCode, responseBodyText);

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }

}
