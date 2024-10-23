using Newtonsoft.Json;
using System.Net;

namespace SimpleGame.GameService.Core.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Process the request
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An unhandled exception occurred.");

                // Handle the exception by returning a standardized response
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Set the content type to JSON and the status code to 500 (Internal Server Error)
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Create a response object with the error details
            var errorResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred. Please try again later.",
                Detailed = ex.Message // Optional: Remove this line in production for security reasons
            };

            // Return the error details as a JSON response
            var result = JsonConvert.SerializeObject(errorResponse);

            return context.Response.WriteAsync(result);
        }
    }
}
