using HR.LeaveManagement.Api.Middleware.Models;
using HR.LeaveManagement.Application.Contracts.Logging;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace HR.LeaveManagement.Api.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IAppLogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _env;
        public GlobalExceptionHandler(IAppLogger<GlobalExceptionHandler> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unexpected error. TraceId: {TraceId}", httpContext.TraceIdentifier);

            var problem = new CustomValidationProblemDetails()
            {
                Title = "Beklenmeyen bir hata oluştu.",
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "ServerError",
                Detail = _env.IsDevelopment() ? exception.ToString() : null
            };
            problem.Extensions["traceId"] = httpContext.TraceIdentifier;

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/problem+json";
            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

            return true;
        }
    }
}
