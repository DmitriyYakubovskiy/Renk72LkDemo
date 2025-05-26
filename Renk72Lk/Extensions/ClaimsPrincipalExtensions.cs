using Renk72Lk.DataAccess.Enums;
using System.Security.Claims;
using Renk72Lk.DataAccess.Extensions;

namespace Renk72Lk.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static bool HasPermissionToAccessFile(this ClaimsPrincipal user, string filePath)
    {
        if (!user.Identity.IsAuthenticated) return false;
        if (user.IsInRole(UserRole.Admin.GetDescription())) return true;

        return true;
    }
}
