using System.ComponentModel;

namespace Renk72Lk.DataAccess.Enums;

public enum PowerDevice
{
    [Description("Ответвление к ВРУ")]
    VruBranch = 1,

    [Description("ЛЭП")]
    PowerLine = 2,

    [Description("ВРУ")]
    Vru = 3,

    [Description("ВЛ")]
    OverheadLine = 4,

    [Description("КЛ")]
    CableLine = 5,

    [Description("ТП-10/0,4кВ")]
    TransformerSubstation = 6
}