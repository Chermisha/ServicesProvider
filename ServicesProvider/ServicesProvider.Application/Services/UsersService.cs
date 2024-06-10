using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServicesProvider.Application.Auth;
using ServicesProvider.Persistence;
using ServicesProvider.Persistence.Entities;
using ServicesProvider.Domain.Enums;
using ServicesProvider.Domain.Models;
using System.Globalization;
using System.Security.Claims;
using ServicesProvider.Persistence;

namespace ServicesProvider.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IConfiguration _configuration;
        public UsersService(ApplicationDbContext context, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IConfiguration configuration)
        {
            _dbContext = context;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _configuration = configuration;
        }

        public async Task<ResponseBase<User>> Register(string email, string password)
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

                return new ResponseBase<User>(0, "Успешно", user);
            }
            else return new ResponseBase<User>(1, "Пользователь уже есть в системе");
        }

        public async Task<ResponseBase<(User, string)>> Login(string email, string password)
        {
            var user = await GetUserByEmail(email);

            if (user == null)
            {
                return new ResponseBase<(User,string)>(1, "Ошибка: email не найден");
            }

            var result = _passwordHasher?.Verify(password, user.Password);

            if (result == false)
            {
                return new ResponseBase<(User, string)>(2, "Ошибка: неверный пароль");
            }

            var token = _jwtProvider.GenerateToken(user);

            return new ResponseBase<(User, string)>(0, "Успешно", (user, token));

        }

        public async Task<User> GetUserById(int id)
        {
            var userEntity = await _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

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
                    Role = (UserRole)userEntity.Role.Id,
                    LastName = userEntity.LastName,
                    FirstName = userEntity.FirstName,
                    MiddleName = userEntity.MiddleName,
                    DateOfBirth = userEntity.DateOfBirth,
                    Address = userEntity.Address,
                    Phone = userEntity.Phone
                };
                //ToDo добавить логику по остальным свойствам?
                return user;
            }
        }

        public async Task<ResponseBase<User>> UpdateUser(int id, User user)
        {
            var updatedCount = await _dbContext.Users
                   .Include(u => u.Role)
                   .Where(u => u.Id == id)
                   .ExecuteUpdateAsync(s => s
                       .SetProperty(u => u.LastName, u => user.LastName)
                       .SetProperty(u => u.FirstName, u => user.FirstName)
                       .SetProperty(u => u.MiddleName, u => user.MiddleName)
                       .SetProperty(u => u.DateOfBirth, u => user.DateOfBirth)
                       .SetProperty(u => u.Address, u => user.Address)
                       .SetProperty(u => u.Phone, u => user.Phone)
                       .SetProperty(u => u.Email, u => user.Email)
                       .SetProperty(u => u.Password, u => user.Password)
                   //TODO: Добавить остальные свойства?
                   );

            if (updatedCount == 0)
            {
                return new ResponseBase<User>(1, "Пользователь не найден");
            }

            var userEntity = await _dbContext.Users.FindAsync(id);

            var userReturn = new User
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                Password = userEntity.Password,
                Role = (UserRole)userEntity.Role.Id,
                LastName = userEntity.LastName,
                FirstName = userEntity.FirstName,
                MiddleName = userEntity.MiddleName,
                DateOfBirth = userEntity.DateOfBirth,
                Address = userEntity.Address,
                Phone = userEntity.Phone
            };
            return new ResponseBase<User>(0, "Успешно", userReturn);
        }

        private async Task<User> GetUserByEmail(string email)
        {
            var userEntity = await _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
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
                    Role = (UserRole)userEntity.Role.Id,
                    LastName = userEntity.LastName,
                    FirstName = userEntity.FirstName,
                    MiddleName = userEntity.MiddleName,
                    DateOfBirth = userEntity.DateOfBirth,
                    Address = userEntity.Address,
                    Phone = userEntity.Phone
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
