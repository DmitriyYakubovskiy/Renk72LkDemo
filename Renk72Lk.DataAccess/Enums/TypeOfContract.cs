using System.ComponentModel;

namespace Renk72Lk.DataAccess.Enums;

public enum TypeOfContract
{
    [Description("Договор энергоснабжения")]
    EnergySupply = 1,

    [Description("Договор купли-продажи электрической энергии")]
    EnergyPurchase = 2,
}
