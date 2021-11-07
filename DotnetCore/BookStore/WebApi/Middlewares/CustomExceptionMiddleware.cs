using System;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public CustomExceptionMiddleware(RequestDelegate next,ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {   
            var watch = Stopwatch.StartNew();

            try
            {
                
                string message = "[Request] HTTP" + context.Request.Method + " - " + context.Request.Path ;
                //Console.WriteLine(message); // console a olan bağımlılığımızdan kurtulmak için bir loggerservice yazdık
                _loggerService.Write(message);
                await _next(context); // buradan sonra validation ve handle hataları fırlatabilir
                watch.Stop();
                message = $"[Response] HTTP {context.Request.Method} - {context.Request.Path} Responded {context.Response.StatusCode} in {watch.Elapsed.TotalMilliseconds} ms ";
                _loggerService.Write(message);  
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
            _loggerService.Write(message);

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