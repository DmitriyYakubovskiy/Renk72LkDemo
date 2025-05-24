using System.ComponentModel;

namespace Renk72Lk.DataAccess.Enums;

public enum ObjectName
{
    [Description("Земельный участок")]
    LandPlot = 1,

    [Description("Административное здание")]
    AdministrativeBuilding = 2,

    [Description("Жилой дом")]
    ResidentialHouse = 3,

    [Description("Гараж")]
    Garage = 4,

    [Description("Комплекс объектов на земельном участке")]
    LandPlotComplex = 5,

    [Description("Многоквартирный жилой дом")]
    ApartmentBuilding = 6,

    [Description("Нежилое помещение")]
    NonResidentialPremises = 7,

    [Description("Нежилое помещение в капитальном строении")]
    NonResidentialInCapitalBuilding = 8,

    [Description("Нежилое помещение в многоквартирном доме")]
    NonResidentialInApartmentBuilding = 9,

    [Description("Строительная площадка")]
    ConstructionSite = 10,

    [Description("Хозяйственное строение")]
    UtilityBuilding = 11
}