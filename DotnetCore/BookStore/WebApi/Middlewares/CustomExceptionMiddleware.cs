using System;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {   
            var watch = Stopwatch.StartNew();

            try
            {
                
                string message = "[Request] HTTP" + context.Request.Method + " - " + context.Request.Path ;
                Console.WriteLine(message);
                await _next(context); // burdan sonra validation ve handle hataları fırlatabilir
                watch.Stop();
                message = $"[Response] HTTP {context.Request.Method} - {context.Request.Path} Responded {context.Response.StatusCode} in {watch.Elapsed.TotalMilliseconds} ms ";
                Console.WriteLine(message);  
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context,watch,ex);
                
            }

            
        }

        private async Task HandleException(HttpContext context, Stopwatch watch, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = $"[Error] HTTP {context.Request.Method} - {context.Response.StatusCode} Error Message : {ex.Message} in {watch.Elapsed.TotalMilliseconds} ms";
            Console.WriteLine(message);

            var JsonString =  JsonSerializer.Serialize(new { Error = ex.Message});
            await context.Response.WriteAsync(JsonString);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder )
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}