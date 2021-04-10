using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Domain.Models;
using API.Infrastructure.Jwt;
using API.Services.Auth.Dtos;
using API.Services.Common;
using Microsoft.AspNetCore.Identity;

namespace API.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(SignInManager<ApplicationUser> signInManager, IJwtGenerator jwtGenerator, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _userManager = userManager;
        }
        public async Task<Response<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new Response<LoginResponse>
                {
                    HttpStatusCode = HttpStatusCode.Unauthorized,
                    Errors = new[] {"Nieprawidłowy email lub hasło"}
                };
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signInResult.Succeeded)
            {
                return new Response<LoginResponse>
                {
                    HttpStatusCode = HttpStatusCode.Unauthorized,
                    Errors = new[] {"Nieprawidłowy email lub hasło"}
                };
            }
            
            var accessToken = await _jwtGenerator.CreateTokenAsync(user);
            var response = new LoginResponse
            {
                AccessToken = accessToken
            };
            return new Response<LoginResponse>
            {
                HttpStatusCode = HttpStatusCode.OK,
                Payload = response
            };
        }

        public async Task<Response<RegisterResponse>> RegisterAsync(RegisterRequest request)
        {
            var userToRegister = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                Age = request.Age,
                Gender = request.Gender,
                HeightInCm = request.HeightInCm,
                WeightInKg = request.WeightInKg,
                CreatedAtUTC = DateTime.UtcNow
            };

            var emailTaken = await _userManager.FindByEmailAsync(request.Email) != null;
            if (emailTaken)
            {
                return new Response<RegisterResponse>
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = new[] {"Adres email jest już zajęty"}
                };
            }

            var createAccountResult = await _userManager.CreateAsync(userToRegister, request.Password);
            if (!createAccountResult.Succeeded)
            {
                return new Response<RegisterResponse>
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = createAccountResult.Errors.Select(e => e.Description)
                };
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(userToRegister, Role.User);
            if (!addToRoleResult.Succeeded)
            {
                return new Response<RegisterResponse>
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Errors = addToRoleResult.Errors.Select(e => e.Description)
                };
            }

            var accessToken = await _jwtGenerator.CreateTokenAsync(userToRegister);
            
            var response = new RegisterResponse
            {
                AccessToken = accessToken
            };

            return new Response<RegisterResponse>
            {
                HttpStatusCode = HttpStatusCode.OK,
                Payload = response
            };
        }
    }
}