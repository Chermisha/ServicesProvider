using ServicesProvider.Models;

namespace ServicesProvider.Services
{
    public interface IUsersService
    {
        Task<(StatusCode, string)> Login(string email, string password);
        Task<StatusCode> Register(string email, string password);
        Task<User> GetUserById(int id);

    }
}