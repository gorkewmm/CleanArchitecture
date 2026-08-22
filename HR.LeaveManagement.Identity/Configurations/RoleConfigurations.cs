using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Identity.Configurations
{
    public class RoleConfigurations : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole()
                {
                    Id = "cde75b0d-3c23-4710-96c7-1429e64fc04e",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole()
                {
                    Id = "644b2a6b-ba2e-4821-ba82-d96d156137b4",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
                );
        }
    }
}
