using Microsoft.AspNetCore.Authorization;
using ServicesProvider.Services;
using System.Security.Claims;

namespace ServicesProvider.Auth
{
    public class UserAuthorizationHandler : AuthorizationHandler<UserRoleRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UserAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
             _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRoleRequirement requirement)
        {

            var userId = context.User.Claims.FirstOrDefault(
                c => c.Type == CustomClaims.UserId
            );

            if (userId == null || !int.TryParse(userId.Value, out var id))
            {
                return;
            }

            using var scope = _serviceScopeFactory.CreateScope();

            var userService = scope.ServiceProvider.GetRequiredService<IUsersService>();

            var user = await userService.GetUserById(id);

            if (user.Role == requirement.UserRole)
            {
                context.Succeed(requirement);
            }

           
        }
    }
}
