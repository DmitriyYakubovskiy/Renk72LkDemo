using System.ComponentModel;

namespace Renk72Lk.DataAccess.Enums;

public enum VoltageClass
{
    [Description("0,23 кВ")]
    V023kV = 1,

    [Description("0,4 кВ")]
    V04kV = 2,

    [Description("6 кВ")]
    V6kV = 3,

    [Description("10 кВ")]
    V10kV = 4
}
