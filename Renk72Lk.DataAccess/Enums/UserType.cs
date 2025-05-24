using System.ComponentModel;

namespace Renk72Lk.DataAccess.Enums;

public enum UserType
{
    [Description("UserType")]
    UserType = 0,

    [Description("Физическое лицо")]
    NaturalPerson = 1,

    [Description("Юридическое лицо")]
    LegalPerson = 2,

    [Description("Индивидуальный предприниматель")]
    IndividualEntrepreneur = 3,
}