using ServicesProvider.Domain.Models;

namespace ServicesProvider.Application.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}