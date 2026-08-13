using HR.LeaveManagement.Api.Middleware.Models;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Excepitons;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace HR.LeaveManagement.Api.ExceptionHandlers
{
    public class NotFoundExceptionHandler : IExceptionHandler
    {
        private readonly IAppLogger<NotFoundExceptionHandler> _logger;
        public NotFoundExceptionHandler(IAppLogger<NotFoundExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if(exception is not NotFoundException notFoundException)
            {
                return false;
            }

            _logger.LogWarning("Kayıt bulunamadı. TraceId: {TraceId}, Mesaj: {Message}",
                                httpContext.TraceIdentifier, notFoundException.Message);

            var problem = new CustomValidationProblemDetails()
            {
                Title = notFoundException.Message,
                Type = nameof(notFoundException),
                Status = (int)HttpStatusCode.NotFound,
                Detail = notFoundException.InnerException?.Message
            };
            problem.Extensions["traceId"] = httpContext.TraceIdentifier;

            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            httpContext.Response.ContentType = "application/problem+json";
            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

            return true;
        }
    }
}
