using System.ComponentModel;

namespace Renk72Lk.DataAccess.Enums;

public enum UserRole
{
    [Description("Admin")]
    Admin = 0,

    [Description("User")]
    User = 1,
}
