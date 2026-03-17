
using Backend.Exceptions;
using System.Net;

namespace Backend.Middleware
{

    public class GlobalExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                int statusCode = StatusCodes.Status500InternalServerError;

                if (ex is EmployeeNotFoundException enfe)
                {
                    statusCode = StatusCodes.Status404NotFound;
                }

                if (ex is EmailAlreadyExistsException eaee)
                {
                    statusCode = StatusCodes.Status409Conflict;
                }

                context.Response.StatusCode = statusCode;

                var response = new
                {
                    statusCode = statusCode,
                    message = ex.Message
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}