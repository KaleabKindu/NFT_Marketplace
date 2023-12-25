using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Model;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.MiddleWares
{
    public class ExceptionHandler
    {

        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly ILogger<ExceptionHandler> _logger;
        public static JsonSerializerOptions options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        private ResponseObject response { get; set; }

        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger, IHostEnvironment env)
        {
            _env = env;
            _next = next;
            _logger = logger;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool failed = false;

            try
            {
                await _next(context);
            }
            catch(AppException ex)
            {
                failed = true;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError(ex, ex.Message);
                response = ResponseObject.Factory(ex.Message);

            }
            catch(Exception ex)
            {
                failed = true;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError(ex, ex.Message);
                response = ResponseObject.Factory("UnKnown Internal Error");
            }
            finally
            {
                if (failed)
                {
                    context.Response.ContentType = "application/json";
                    var json = JsonSerializer.Serialize(response, options);
                    await context.Response.WriteAsync(json);

                }

            }
        }


    }
}



