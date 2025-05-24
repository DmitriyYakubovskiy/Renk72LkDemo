using System.ComponentModel;

namespace Renk72Lk.DataAccess.Enums;

public enum PassportType
{
    [Description("Паспорт гражданина РФ")]
    PassportOfCitizenRF = 1,

    [Description("Паспорт иностранного гражданина")]
    PassportOfForeignCitizen = 2,
}