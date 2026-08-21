using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Identity.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection IdentityConfigurations(this IServiceCollection service,
            IConfiguration config)
        {
            service.AddScoped<IAuthService, AuthService>();

            service.Configure<JwtSettings>(
                config.GetSection("JwtSettings")
                );

            return service;
        }
    }
}
