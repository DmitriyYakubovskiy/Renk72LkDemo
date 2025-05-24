using System.ComponentModel;

namespace Renk72Lk.DataAccess.Enums;

public enum TechnologicalConnectionType
{
    [Description("Технологическое присоединение энергопринимающих устройств до 15 кВт")]
    UpTo15kW = 15,

    [Description("Технологическое присоединение энергопринимающих устройств до 150 кВт")]
    UpTo150kW = 100,

    [Description("Временное технологическое присоединение")]
    Temporary = 999
}
