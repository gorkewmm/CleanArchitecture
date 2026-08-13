using HR.LeaveManagement.Api.Middleware.Models;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Excepitons;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace HR.LeaveManagement.Api.ExceptionHandlers
{
    public class BadRequestExceptionHandler : IExceptionHandler
    {
        private readonly IAppLogger<BadRequestExceptionHandler> _logger;
        public BadRequestExceptionHandler(IAppLogger<BadRequestExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not BadRequestException badRequestException)
            {
                return false;
            }

            _logger.LogWarning("BadRequest hatası. TraceId: {TraceId}, Mesaj: {Message}",
                               httpContext.TraceIdentifier, badRequestException.Message);

            var problem = new CustomValidationProblemDetails()
            {
                Errors = badRequestException.ValidationErrors,
                Title = badRequestException.Message,
                Status = (int)HttpStatusCode.BadRequest,
                Detail = badRequestException.InnerException?.Message,
                Type = nameof(badRequestException)
            };
            problem.Extensions["traceId"] = httpContext.TraceIdentifier;

            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problem);

            return true;
        }
    }
}