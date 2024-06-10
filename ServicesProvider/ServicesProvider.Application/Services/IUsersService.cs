using ServicesProvider.Domain.Models;

namespace ServicesProvider.Application.Services
{
    public interface IUsersService
    {
        Task<User> GetUserById(int id);
        Task<ResponseBase<(User, string)>> Login(string email, string password);
        Task<ResponseBase<User>> Register(string email, string password);
        Task<ResponseBase<User>> UpdateUser(int id, User user);
    }
}