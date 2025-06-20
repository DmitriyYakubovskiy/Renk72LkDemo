using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.SignalR;
using Renk72Lk.DataAccess.Enums;
using Renk72Lk.DataAccess.Extensions;
using Renk72Lk.Models.DataBase;
using Renk72Lk.Services.DataBase;
using Renk72Lk.Services.Email;

namespace Renk72Lk.Hubs;

[Authorize("NotBannedPolicy")]
public class ChatHub : Hub
{
    private readonly LinkGenerator linkGenerator;
    private readonly IMessageService chatService;
    private readonly IUserService userService;
    private readonly IBidService bidService;
    private readonly IRabbitMQProducerSerivce emailService;
    private readonly HttpContext httpContext;

    public ChatHub(IMessageService chatService, IUserService userService, IRabbitMQProducerSerivce emailService,
                  IBidService bidService, IFileService fileService, IModelMetadataProvider metadataProvider,
                  LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
    {
        this.linkGenerator = linkGenerator;
        this.chatService = chatService;
        this.userService = userService;
        this.bidService = bidService;
        this.emailService = emailService;
        this.httpContext = httpContextAccessor.HttpContext!;
    }

    public async Task JoinBidGroup(int userId, int bidId)
    {
        var user = await userService.GetByUserNameAsync(Context.User.Identity.Name);
        var bid = bidService.GetById(bidId);

        if (user.Id != userId || (bid?.User?.Id != userId && !(await userService.GetUserRolesAsync(user.Id)).Contains(UserRole.Admin.GetDescription())))
        {
            await Clients.Caller.SendAsync("ReceiveError", "Нет доступа");
            return;
        }
        await Groups.AddToGroupAsync(Context.ConnectionId, $"bid-{bidId}");
    }

    public async Task LeaveBidGroup(int bidId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"bid-{bidId}");
    }

    public async Task SendMessage(int bidId, string message, int userId, int? fileId = null, string? fileName = null, string? filePath = null)
    {
        if (String.IsNullOrEmpty(message) && fileId == null)
        {
            await Clients.Caller.SendAsync("ReceiveError", "Сообщение не может быть пустым");
            return;
        }

        var user = await userService.GetByUserNameAsync(Context.User.Identity.Name);
        var bid = bidService.GetById(bidId);

        if (user.Id != userId || (bid?.User?.Id != userId && !(await userService.GetUserRolesAsync(user.Id)).Contains(UserRole.Admin.GetDescription())))
        {
            await Clients.Caller.SendAsync("ReceiveError", "Нет доступа");
            return;
        }

        var messageModel = new MessageModel
        {
            Message = message,
            BidId = bidId,
            UserId = userId,
            FileId = fileId
        };

        messageModel = await chatService.CreateAsync(messageModel);
        var relativeUrl = linkGenerator.GetPathByAction("GetById", "Bid", new { id = bidId });
        var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";

        var url = $"{baseUrl}{relativeUrl}";

        if (user?.Id == bid.UserId)
        {
            await emailService.NotifyAdminAboutNewMessage(bid.Id, url!);
        }
        else
        {
            var bidUser = await userService.GetByIdAsync(bid.UserId.Value);
            await emailService.NotifyUserAboutNewMessage(bidUser, bid.Id, url!);
        }

        await Clients.Group($"bid-{bidId}").SendAsync("ReceiveMessage", new
        {
            id = messageModel.Id,
            message = messageModel.Message,
            userId = messageModel.UserId,
            createdAt = messageModel.CreatedAt,
            user = new
            {
                surname = user.Surname,
                name = user.Name,
                patronymic = user.Patronymic
            },
            file = fileId != null ? new
            {
                id = fileId,
                name = fileName,
                path = filePath
            } : null
        });
    }

    [Authorize(Roles = "Admin")]
    public async Task DeleteMessage(int messageId, int userId)
    {
        var bidId = GetBidIdForMessage(messageId);
        chatService.Delete(messageId);

        await Clients.Group($"bid-{bidId}").SendAsync("MessageDeleted", messageId);
    }

    private int GetBidIdForMessage(int messageId)
    {
        return chatService.GetById(messageId)?.BidId ?? 0;
    }
}

