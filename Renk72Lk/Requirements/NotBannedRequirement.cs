using Microsoft.AspNetCore.Authorization;

namespace Renk72Lk.Requirements;

public class NotBannedRequirement : IAuthorizationRequirement
{
    public NotBannedRequirement()
    {
    }
}
