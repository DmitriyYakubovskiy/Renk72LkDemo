using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renk72Lk.DataAccess.Enums;
using Renk72Lk.DataAccess.Extensions;
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
    private readonly IMessageService chatService;
    private readonly IUserService userService;
    private readonly IBidService bidService;
    private readonly IEmailSerivce emailService;
    private readonly IFileService fileService;

    public ChatController(IMessageService ticketService, IUserService userService, IBidService userBidService,
       IEmailSerivce emailService, IFileService fileService)
    {
        this.chatService = ticketService;
        this.userService = userService;
        this.bidService = userBidService;
        this.emailService = emailService;
        this.fileService = fileService;
    }

    [HttpPost("SendMessage")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SendMessage([FromForm] MessageViewModel model)
    {
        var user = await userService.GetByUserNameAsync(User.Identity.Name!);
        var bid = bidService.GetById(model.BidId);
        var url = Url.Action("GetById", "Bid", new { id = bid?.Id }, Request.Scheme);

        if (user.Id != model.UserId) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
        if (bid?.User?.Id != model.UserId && !(await userService.GetUserRolesAsync(user.Id)).Contains(UserRole.Admin.GetDescription())) return base.BadRequest(ResultModel.GetErrors(["Нет доступа"]));
        try
        {
            if (model.DocFiles.Count > 0)
            {
                foreach (var file in model.DocFiles)
                {
                    int id = await fileService.CreateMessageFileAsync(file);
                    await chatService.CreateAsync(new Models.DataBase.MessageModel() { Message = "Прикреплен файл.", BidId = model.BidId, UserId = model.UserId, FileId = id });
                }
            }
            if (!string.IsNullOrEmpty(model.Text)) await chatService.CreateAsync(new Models.DataBase.MessageModel() { Message = model.Text, BidId = model.BidId, UserId = model.UserId });

            if (user?.Id == bid.UserId)
            {
                await emailService.NotifyAdminAboutNewMessage(MetadataProvider, ModelState, bid.Id, url!);
            }
            else
            {
                var bidUser = await userService.GetByIdAsync(bid.UserId.Value);

                await emailService.NotifyUserAboutNewMessage(MetadataProvider, ModelState, bidUser, bid.Id, url!);
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ResultModel.GetErrors([$"Ошибка: {ex.Message}"]));
        }
    }

    [HttpPost("Delete")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete([FromForm] int id)
    {
        chatService.Delete(id);
        return Ok();
    }
}
