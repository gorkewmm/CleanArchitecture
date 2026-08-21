using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Excepitons;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HR.LeaveManagement.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInanager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInanager;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                throw new NotFoundException($"User {request.Email} not found", request.Email);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                throw new BadRequestException($"Password is not valid");
            }

            var jwtSecurityToken = await GenerateToken(user);
            var handler = new JwtSecurityTokenHandler();

            var response = new AuthResponse()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Token = handler.WriteToken(jwtSecurityToken)
            };

            return response;
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleToClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid", user.Id),
            }.Union(userClaims).Union(roleToClaims);


            var symetricSecKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symetricSecKey, SecurityAlgorithms.HmacSha256);


            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                claims : claims,
                signingCredentials : signingCredentials
                );
            return jwtSecurityToken;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is not null)
            {
                throw new BadRequestException($"Email '{request.Email}' is already registered.");
            }

            var appUser = new ApplicationUser()
            {
                FirstName= request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Username,
            };

            var result = await _userManager.CreateAsync(appUser, request.Password);
            if (!result.Succeeded)
            {
                throw new BadRequestException($"Failed");
            }


            return new RegistrationResponse()
            {
                UserId = user.Id
            };

        }
    }
}
