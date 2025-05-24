using System.ComponentModel;

namespace Renk72Lk.DataAccess.Enums;

public enum ReliabilityCategory
{
    [Description("I - два независимых взаимно резервирующих источников питания, и дополнительное питание от третьего независимого взаимно резервирующего источника питания")]
    Category1 = 1,

    [Description("II - два независимых взаимно резервирующих источников питания")]
    Category2,

    [Description("III - один источник питания")]
    Category3
}