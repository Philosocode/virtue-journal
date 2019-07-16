using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

// FROM: https://code-maze.com/global-error-handling-aspnetcore/
namespace VirtueApi.Data
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            // Customize here
            // e.g. catch (FileNotFoundException), check the error code
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
 
            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error"
            }.ToString());
        }
    }
}