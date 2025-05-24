using System.ComponentModel;

namespace Renk72Lk.DataAccess.Enums;

public enum ReasonForBid
{
    [Description("Новое технологическое присоединение впервые вводимое в эксплуатацию энергопринимающего устройства")]
    NewConnection = 1,

    [Description("Увеличение мощности ранее присоединенных энергопринимающего устройства")]
    PowerIncrease,

    [Description("Изменение категории надежности электроснабжения")]
    ReliabilityCategoryChange,

    [Description("Изменение точки присоединения")]
    ConnectionPointChange
}