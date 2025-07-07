using Microsoft.AspNetCore.Authorization;
using Renk72Lk.DataAccess.Enums;

namespace Renk72Lk.Handlers;

public class NotBannedHandler : AuthorizationHandler<NotBannedRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,NotBannedRequirement requirement)
    {
        var user = context.User;

        if (user.Identity?.IsAuthenticated == true && !user.HasClaim(UserBanned.UserBanned.ToString(), "true"))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public class NotBannedRequirement : IAuthorizationRequirement
{
    public NotBannedRequirement()
    {
    }
}
