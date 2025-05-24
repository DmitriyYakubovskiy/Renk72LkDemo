using System.ComponentModel;

namespace Renk72Lk.DataAccess.Enums;

public enum ConnectionType
{
    [Description("Постоянное электроснабжение")]
    Permanent = 1,

    [Description("Временное электроснабжение")]
    Temporary = 2,

    [Description("Временное электроснабжение передвижного объекта")]
    MobileTemporary = 3
}
