using System.ComponentModel;

namespace Renk72Lk.DataAccess.Enums;

public enum GuarantySupplier
{
    [Description("АО «Газпром энергосбыт Тюмень»")]
    Gazprom = 1,
    
    [Description("АО «ЭК Восток»")]
    Vostok = 2,
    
    [Description("Другой")]
    Other = 0
}
