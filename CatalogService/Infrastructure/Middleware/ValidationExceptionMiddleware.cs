using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace CatalogService.Infrastructure.Middleware
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var errors = ex.Errors.Select(e => new
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                }).ToList();

                var result = new
                {
                    type = "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                    title = "One or more validation errors occurred.",
                    status = 400,
                    errors = errors
                };

                var json = JsonSerializer.Serialize(result);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
