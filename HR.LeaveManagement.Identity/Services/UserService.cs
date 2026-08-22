using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Employee> GetEmployee(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            

            return new Employee()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var users = await _userManager.GetUsersInRoleAsync("Employee");

            var employeeList = new List<Employee>();

            foreach (var user in users)
            {
                var employee =new Employee()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                employeeList.Add(employee);
            }

            return employeeList;
        }
    }
}
