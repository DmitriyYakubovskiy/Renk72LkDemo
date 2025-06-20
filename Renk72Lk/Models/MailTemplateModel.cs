using Microsoft.AspNetCore.Mvc;

namespace Renk72Lk.Models;

[ApiExplorerSettings(IgnoreApi = true)]
public class MailTemplateModel
{
    public string? Title { get; set; }
    public Subtitle? SubTitle { get; set; }
    public string? Login { get; set; }
    public string? Email { get; set; }
    public string? Telephone { get; set; }
    public MailBlockWithLink? Button { get; set; }
    public MailBlockWithLink? AddButton { get; set; } = null;
    public string? Text { get; set; }
    public string? PreFooter { get; set; } = "ООО «РЭНК»";
    public MailBlockWithLink? ExtraPreFooter { get; set; } = null;
    public string? Footer { get; set; } = $"© {DateTime.Now.Year} ВСЕ ПРАВА ЗАЩИЩЕНЫ​ | ООО «РЭНК»";
}

[ApiExplorerSettings(IgnoreApi = true)]
public class Subtitle
{
    public string? Text { get; set; }
    public string? BoldText { get; set; }
}

[ApiExplorerSettings(IgnoreApi = true)]
public class MailBlockWithLink
{
    public string? Text { get; set; }
    public string? Url { get; set; }
}
