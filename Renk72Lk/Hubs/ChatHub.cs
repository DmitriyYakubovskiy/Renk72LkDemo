using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Renk72Lk.DataAccess.Enums;
using Renk72Lk.DataAccess.Extensions;
using Renk72Lk.Services.DataBase;
using Renk72Lk.Models.DataBase;

namespace Renk72Lk.Hubs;

[Authorize("NotBannedPolicy")]
public class ChatHub : Hub
{
    private readonly IMessageService chatService;
    private readonly IUserService userService;
    private readonly IBidService bidService;
    private readonly IFileService fileService;

    public ChatHub(IMessageService chatService, IUserService userService,
                  IBidService bidService, IFileService fileService)
    {
        this.chatService = chatService;
        this.userService = userService;
        this.bidService = bidService;
        this.fileService = fileService;
    }

    public async Task JoinBidGroup(int bidId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"bid-{bidId}");
    }

    public async Task LeaveBidGroup(int bidId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"bid-{bidId}");
    }

    public async Task SendMessageToBid(int bidId, string message, int userId)
    {
        Console.WriteLine($"{message} {bidId}, {userId}");
        var user = await userService.GetByIdAsync(userId);
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
            UserId = userId
        };

        messageModel = await chatService.CreateAsync(messageModel);

        await Clients.Group($"bid-{bidId}").SendAsync("ReceiveMessage", new
        {
            Id = messageModel.Id,
            Message = messageModel.Message,
            UserId = messageModel.UserId,
            CreatedAt = messageModel.CreatedAt,
            User = new
            {
                Surname = user.Surname,
                Name = user.Name,
                Patronymic = user.Patronymic
            }
        });
    }

    public async Task DeleteMessage(int messageId, int userId)
    {
        var user = await userService.GetByIdAsync(userId);
        if (!(await userService.GetUserRolesAsync(user.Id)).Contains(UserRole.Admin.GetDescription()))
        {
            await Clients.Caller.SendAsync("ReceiveError", "Нет прав для удаления");
            return;
        }

        chatService.Delete(messageId);
        await Clients.Group($"bid-{GetBidIdForMessage(messageId)}").SendAsync("MessageDeleted", messageId);
    }

    private int GetBidIdForMessage(int messageId)
    {
        return chatService.GetById(messageId)?.BidId ?? 0;
    }
}

