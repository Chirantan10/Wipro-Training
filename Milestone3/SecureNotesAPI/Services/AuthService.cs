using Microsoft.AspNetCore.Identity;
using SecureNotesAPI.Data;
using SecureNotesAPI.Models;
using SecureNotesAPI.Services;

namespace SecureNotesAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterRequest model)
        {
            var existingUser = await _userManager.FindByNameAsync(model.Username);
            if (existingUser != null)
            {
                return (false, "Username already exists");
            }

            var user = new User
            {
                UserName = model.Username,
                Email = $"{model.Username}@notes.com", // Simple email generation
                DisplayName = model.DisplayName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return (false, $"Registration failed: {errors}");
            }

            return (true, "User registered successfully. Please log in.");
        }

        public async Task<(bool Success, string Token, UserResponse User)> LoginAsync(LoginRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return (false, null, null);
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isValidPassword)
            {
                return (false, null, null);
            }

            var token = _tokenService.GenerateToken(user);
            var userResponse = new UserResponse
            {
                Username = user.UserName,
                DisplayName = user.DisplayName
            };

            return (true, token, userResponse);
        }
    }
}