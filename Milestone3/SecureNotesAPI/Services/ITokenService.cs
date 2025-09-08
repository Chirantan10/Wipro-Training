using SecureNotesAPI.Models;

namespace SecureNotesAPI.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}