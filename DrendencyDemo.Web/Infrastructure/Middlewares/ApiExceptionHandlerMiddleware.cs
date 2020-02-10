using DrendencyDemo.Web.Models.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using TrendencyDemo.Common.TrendencyDemoExceptions;

namespace DrendencyDemo.Web.Infrastructure.Middlewares
{
    public class ApiExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private ILogger<ApiExceptionHandlerMiddleware> _logger;

        public ApiExceptionHandlerMiddleware(RequestDelegate next,
            IWebHostEnvironment env,
            ILogger<ApiExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (TrendencyDemoException tEx)
            {
                _logger.LogInformation(tEx, $"Status: {tEx.TrendencyDemoStatusCode.ToString()}");
                var payload = new TrendencyDemoExceptionDto
                {
                    TrendencyDemoStatusCode = tEx.TrendencyDemoStatusCode,
                    Message = tEx.Message,
                    Exception = _env.IsDevelopment()
                        ? tEx.ToString()
                        : "Exception available only in development environment. Check the logs for details."
                };
                await WriteAsJsonAsync(context, tEx.HttpStatusCode, payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                var payload = new InternalServerErrorDto
                {
                    Message = "Internal Server Error. Exception has been logged",
                    Exception = _env.IsDevelopment()
                        ? ex.ToString()
                        : "Exception available only in development environment. Check the logs for details."
                };
                await WriteAsJsonAsync(context, HttpStatusCode.InternalServerError, payload);
            }
        }

        private async Task WriteAsJsonAsync(HttpContext context, HttpStatusCode statusCode, object payload, bool clearBeforeWrite = true)
        {
            if (clearBeforeWrite)
            {
                context.Response.Clear();
            }
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            var json = JsonConvert.SerializeObject(payload);
            context.Response.ContentLength = json.Length;
            await context.Response.WriteAsync(json);
        }
    }
}
