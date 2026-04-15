using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Orders.Domain.Common;

namespace Orders.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken ct)
        {
            var (statusCode, message) = exception switch
            {
                DomainException => 
                (StatusCodes.Status400BadRequest, exception.Message),

                ValidationException ex => 
                (StatusCodes.Status400BadRequest, string.Join(", ", ex.Errors.Select(e => e.ErrorMessage))),

                _ => (StatusCodes.Status500InternalServerError, "Internal sever error")
            };

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(new
            {
                error = message
            }, ct);

            return true;
        }
    }
}
