using Core.Errors;
using Core.Exceptions;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IHostEnvironment _host;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment host)
        {
            _next = next;
            _logger = logger;
            _host = host;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);


            }
            catch (BadRequestException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex);
            }
            catch (UserUnauthorizedException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.Unauthorized, ex);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.NotFound, ex);
            }
            catch (ConflictException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.Conflict, ex);
            }
   
            catch (Exception ex)
            {
                _logger.LogError(ex.Message , ex.StackTrace);
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var errorResponse = CreateErrorResponse(statusCode, ex);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var jsonResponse = JsonSerializer.Serialize(errorResponse, options);

            await context.Response.WriteAsync(jsonResponse);
        }

        private object CreateErrorResponse(HttpStatusCode statusCode, Exception ex)
        {
            if(ex is BadRequestException apiEX && apiEX.Errors is not null)
            {
                return new ApiValidationErrorResponse(apiEX.Errors);
            }
            if (ex is BaseApiException apiEx)
            {
                return new ApiErrorResponse(apiEx.StatusCode, apiEx.Message);
            }

            return _host.IsDevelopment()
                ? new ApiException((int)statusCode, ex.Message, ex.StackTrace?.ToString())
                : new ApiException((int)statusCode, "Internal Server Error");
        }
    }
}
