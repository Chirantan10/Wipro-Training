using SecureNotesAPI.Models;

namespace SecureNotesAPI.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Message)> RegisterAsync(RegisterRequest model);
        Task<(bool Success, string Token, UserResponse User)> LoginAsync(LoginRequest model);
    }
}