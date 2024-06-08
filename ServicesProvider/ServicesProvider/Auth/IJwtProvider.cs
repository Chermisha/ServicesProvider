using ServicesProvider.Models;

namespace ServicesProvider.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}