using Microsoft.AspNetCore.Authorization;
using ServicesProvider.Domain.Enums;

namespace ServicesProvider.Application.Auth
{
    public class UserRoleRequirement:IAuthorizationRequirement
    {
        public UserRole UserRole { get; set; }
    }
}
