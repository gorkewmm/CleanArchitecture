using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Identity.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser()
                {
                    Id = "335008de-39e9-40fe-a89a-989e2c48997a",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    UserName = "admin@localhost.com",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    PasswordHash = hasher.HashPassword(null,"P@ssword"),
                    EmailConfirmed = true

                },
                new ApplicationUser()
                {

                },
                new ApplicationUser()
                {

                }
                
                );
        }
    }
}
