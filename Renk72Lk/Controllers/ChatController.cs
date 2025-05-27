using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Renk72Lk.DataAccess.Enums;
using Renk72Lk.DataAccess.Extensions;
using Renk72Lk.Hubs;
using Renk72Lk.Models;
using Renk72Lk.Services;
using Renk72Lk.Services.DataBase;
using Renk72Lk.ViewModels;

namespace Renk72Lk.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize("NotBannedPolicy")]
public class ChatController : Controller
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IEmailSerivce _emailService;
    private readonly IFileService _fileService;

    public ChatController(IHubContext<ChatHub> hubContext, IEmailSerivce emailService, IFileService fileService)
    {
        _hubContext = hubContext;
        _emailService = emailService;
        _fileService = fileService;
    }

    //[HttpPost("SendFile")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> SendFile([FromForm] IFormFile file, [FromForm] int bidId, [FromForm] int userId)
    //{
    //    try
    //    {
    //        int fileId = await _fileService.CreateMessageFileAsync(file);

    //        // Отправка через Hub
    //        await _hubContext.Clients.Group($"bid-{bidId}").SendAsync("ReceiveMessage", new
    //        {
    //            Message = "Прикреплен файл",
    //            FileId = fileId,
    //            FileName = file.FileName,
    //            FilePath = $"/files/{fileId}",
    //            UserId = userId,
    //            CreatedAt = DateTime.Now
    //        });

    //        return Ok();
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ResultModel.GetErrors([$"Ошибка: {ex.Message}"]));
    //    }
    //}
}
