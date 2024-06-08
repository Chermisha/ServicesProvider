using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Validations;
using ServicesProvider.Auth;
using ServicesProvider.Data;
using ServicesProvider.Data.Entities;
using ServicesProvider.Enums;
using ServicesProvider.Models;
using System.Globalization;
using System.Security.Claims;

namespace ServicesProvider.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IConfiguration _configuration;
        //private readonly AuthorizationHandlerContext _authorizationHandlerContext;
        public UsersService(ApplicationDbContext context, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IConfiguration configuration)
        {
            _dbContext = context;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _configuration = configuration;
            //_authorizationHandlerContext = authorizationHandlerContext;
        }

        public async Task<StatusCode> Register(string email, string password)
        {
            var userForCheck = await GetUserByEmail(email);
            if (userForCheck == null)
            {
                var passwordHash = _passwordHasher.Generate(password);
               
                var userEntity = await AddUser(email, passwordHash);

                var user = new User
                {
                    Id = userEntity.Id,
                    Email = userEntity.Email,
                    Password = userEntity.Password,
                    Role = (UserRole)userEntity.Role.Id

                };

                return new StatusCode(0, "Успешно");
            }
            else return new StatusCode(1, "Пользователь уже есть в системе");
        }

        public async Task<(StatusCode, string)> Login(string email, string password)
        {
            var user = await GetUserByEmail(email);

            if (user == null)
            {
                return (new StatusCode(1, "Ошибка: email не найден"), null);
            }

            var result = _passwordHasher?.Verify(password, user.Password);

            if (result == false)
            {
                return (new StatusCode(2, "Ошибка: неверный пароль"), null);
            }

            var token = _jwtProvider.GenerateToken(user);

            return (new StatusCode(0, "Успешно"), token);

        }

        public async Task<User> GetUserById (int id)
        {
            var userEntity = _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Id == id);

            if (userEntity == null)
            {
                return null;
            }
            else
            {
                var user = new User
                {
                    Id = userEntity.Id,
                    Email = userEntity.Email,
                    Password = userEntity.Password,
                    Role = (UserRole)userEntity.Role.Id
                    //ToDo добавить логику по остальным свойствам?
                };
                return user;
            };
        }

        //private async Task Authorize()
        //{
        //    var userId = _authorizationHandlerContext.User.FindFirst(ClaimTypes.)
        //        c => c.Type == C)



        //}

        private async Task<User> GetUserByEmail(string email)//публичный?
        {
            var userEntity = _dbContext.Users
                .FirstOrDefault(u => u.Email == email);
            if (userEntity == null)
            {
                return null;
            }
            else
            {
                var user = new User
                {
                    Id = userEntity.Id,
                    Email = userEntity.Email,
                    Password = userEntity.Password,
                    //ToDo добавить логику по остальным свойствам?
                };
                return user;
            };

        }

        private async Task<UserEntity> AddUser(string email, string password)
        {
            var role = new UserRole();
            var providerEmails = _configuration.GetSection("Authorization:ProviderEmail").GetChildren().Select(c => c.Value).ToArray();
            foreach (var providerEmail in providerEmails)
            {
                if (email == providerEmail)
                {
                    role = UserRole.Provider;
                    break;
                }
                else
                {
                    role = UserRole.Client;
                }
            }

            var roleEntity = await _dbContext.UserRoles.SingleOrDefaultAsync(r => r.Id == (int)role);

            var userEntity = new UserEntity
            {
                Email = email,
                Password = password,
                Role = roleEntity
            };
    
            await _dbContext.Users.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();
            return userEntity;
        }

    }
}
