namespace Renk72Lk.Helpers;

public class TicketStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public static readonly TicketStatus[] ticketStatuses = new[]
    {
        new TicketStatus { Id = 0, Name = "В ожидании" },
        new TicketStatus { Id = 1, Name = "На согласовании" },
        new TicketStatus { Id = 2, Name = "Закрыто" },
        new TicketStatus { Id = 3, Name = "В работе" },
        new TicketStatus { Id = 4, Name = "Фактическое подключение" },
        new TicketStatus { Id = 5, Name = "Аннулирована" },        
        new TicketStatus { Id = 6, Name = "Замечания" },
        new TicketStatus { Id = 7, Name = "Счет на оплате" },        
        new TicketStatus { Id = 8, Name = "Выполнение ТУ заявителем" },
        new TicketStatus { Id = 9, Name = "Выполнение ТУ сетевой организацией" }
    };
}
