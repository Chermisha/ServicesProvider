using Microsoft.AspNetCore.Authorization;
using ServicesProvider.Enums;

namespace ServicesProvider.Auth
{
    public class UserRoleRequirement:IAuthorizationRequirement
    {
        public UserRole UserRole { get; set; }
    }
}
