using Microsoft.AspNetCore.Authorization;
using Renk72Lk.DataAccess.Enums;
using Renk72Lk.DataAccess.Extensions;
using Renk72Lk.Requirements;

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