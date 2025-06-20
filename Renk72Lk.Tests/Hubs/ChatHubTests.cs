using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Moq;
using Renk72Lk.Hubs;
using Renk72Lk.Models.DataBase;
using Renk72Lk.Services.DataBase;
using Renk72Lk.Services.Email;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Renk72Lk.Tests.Hubs;

public class ChatHubTests
{
    private readonly Mock<IMessageService> _chatService = new();
    private readonly Mock<IUserService> _userService = new();
    private readonly Mock<IRabbitMQProducerSerivce> _emailService = new();
    private readonly Mock<IBidService> _bidService = new();
    private readonly Mock<IFileService> _fileService = new();
    private readonly Mock<IModelMetadataProvider> _metadataProvider = new();
    private readonly Mock<LinkGenerator> _linkGenerator = new();
    private readonly Mock<IHubCallerClients> _clients = new();
    private readonly Mock<IGroupManager> _groups = new();
    private readonly Mock<HubCallerContext> _context = new();
    private readonly Mock<ISingleClientProxy> _singleClientProxy = new();
    private readonly Mock<IClientProxy> _groupClientProxy = new();

    private ChatHub CreateHub(ClaimsPrincipal? user = null)
    {
        var hub = new ChatHub(
            _chatService.Object,
            _userService.Object,
            _emailService.Object,
            _bidService.Object,
            _fileService.Object,
            _metadataProvider.Object,
            _linkGenerator.Object
        );

        typeof(Hub).GetProperty("Clients")!.SetValue(hub, _clients.Object);
        typeof(Hub).GetProperty("Groups")!.SetValue(hub, _groups.Object);
        typeof(Hub).GetProperty("Context")!.SetValue(hub, _context.Object);

        if (user != null)
        {
            _context.Setup(c => c.User).Returns(user);
            _context.Setup(c => c.ConnectionId).Returns("test-connection");
        }

        return hub;
    }

    private ClaimsPrincipal GetUserPrincipal(string name = "testuser")
    {
        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, name) });
        return new ClaimsPrincipal(identity);
    }

    [Fact]
    public async Task JoinBidGroup_AccessDenied_SendsError()
    {
        // Arrange
        var user = new UserModel { Id = 1 };
        var bid = new BidModel { Id = 2, User = new UserModel { Id = 2 } };
        _userService
            .Setup(s => s.GetByUserNameAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(user);

        _bidService
            .Setup(s => s.GetById(
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
            .Returns(bid);
        _userService.Setup(s => s.GetUserRolesAsync(user.Id)).ReturnsAsync(new List<string>());

        _clients.Setup(c => c.Caller).Returns(_singleClientProxy.Object);

        var hub = CreateHub(GetUserPrincipal());

        // Act
        await hub.JoinBidGroup(1, 2);

        // Assert
        _singleClientProxy.Verify(c => c.SendAsync("ReceiveError", "Нет доступа", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task JoinBidGroup_AccessGranted_AddsToGroup()
    {
        // Arrange
        var user = new UserModel { Id = 1 };
        var bid = new BidModel { Id = 2, User = new UserModel { Id = 1 } };
        _userService
            .Setup(s => s.GetByUserNameAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(user);
        _bidService
            .Setup(s => s.GetById(
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
            .Returns(bid);

        _clients.Setup(c => c.Caller).Returns(_singleClientProxy.Object);
        _groups.Setup(g => g.AddToGroupAsync("test-connection", "bid-2", It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var hub = CreateHub(GetUserPrincipal());

        // Act
        await hub.JoinBidGroup(1, 2);

        // Assert
        _groups.Verify(g => g.AddToGroupAsync("test-connection", "bid-2", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task LeaveBidGroup_RemovesFromGroup()
    {
        // Arrange
        _groups.Setup(g => g.RemoveFromGroupAsync("test-connection", "bid-2", It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        var hub = CreateHub(GetUserPrincipal());

        // Act
        await hub.LeaveBidGroup(2);

        // Assert
        _groups.Verify(g => g.RemoveFromGroupAsync("test-connection", "bid-2", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SendMessage_EmptyMessageAndNoFile_SendsError()
    {
        // Arrange
        _clients.Setup(c => c.Caller).Returns(_singleClientProxy.Object);
        var hub = CreateHub(GetUserPrincipal());

        // Act
        await hub.SendMessage(1, "", 1);

        // Assert
        _singleClientProxy.Verify(c => c.SendAsync("ReceiveError", "Сообщение не может быть пустым", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SendMessage_AccessDenied_SendsError()
    {
        // Arrange
        var user = new UserModel { Id = 1 };
        var bid = new BidModel { Id = 2, User = new UserModel { Id = 2 } };
        _userService
            .Setup(s => s.GetByUserNameAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(user);
        _bidService
            .Setup(s => s.GetById(
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
            .Returns(bid);
        _userService.Setup(s => s.GetUserRolesAsync(user.Id)).ReturnsAsync(new List<string>());

        _clients.Setup(c => c.Caller).Returns(_singleClientProxy.Object);

        var hub = CreateHub(GetUserPrincipal());

        // Act
        await hub.SendMessage(2, "msg", 1);

        // Assert
        _singleClientProxy.Verify(c => c.SendAsync("ReceiveError", "Нет доступа", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SendMessage_AccessGranted_SendsMessageAndNotifies()
    {
        // Arrange
        var user = new UserModel { Id = 1, Surname = "Иванов", Name = "Иван", Patronymic = "Иванович" };
        var bid = new BidModel { Id = 2, User = user, UserId = 1 };
        var messageModel = new MessageModel { Id = 10, Message = "msg", UserId = 1, CreatedAt = DateTime.Now };

        _userService
            .Setup(s => s.GetByUserNameAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(user);
        _bidService
            .Setup(s => s.GetById(
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
            .Returns(bid);
        _userService.Setup(s => s.GetUserRolesAsync(user.Id)).ReturnsAsync(new List<string>());
        _chatService.Setup(s => s.CreateAsync(It.IsAny<MessageModel>())).ReturnsAsync(messageModel);
        _linkGenerator
            .Setup(l => l.GetPathByAction(
                "GetById",
                "Bid",
                It.IsAny<object>(),
                It.IsAny<PathString>(),
                It.IsAny<FragmentString>(),
                It.IsAny<LinkOptions>()))
            .Returns("/bid/2");
        _emailService.Setup(e => e.NotifyAdminAboutNewMessage(bid.Id, "/bid/2")).Returns(Task.CompletedTask);

        _clients.Setup(c => c.Group("bid-2")).Returns(_groupClientProxy.Object);

        var hub = CreateHub(GetUserPrincipal());

        // Act
        await hub.SendMessage(2, "msg", 1);

        // Assert
        _groupClientProxy.Verify(g => g.SendAsync("ReceiveMessage", It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Once);
        _emailService.Verify(e => e.NotifyAdminAboutNewMessage(bid.Id, "/bid/2"), Times.Once);
    }

    [Fact]
    public async Task DeleteMessage_DeletesAndNotifies()
    {
        // Arrange
        var messageId = 5;
        var bidId = 7;
        _chatService.Setup(s => s.GetById(messageId)).Returns(new MessageModel { BidId = bidId });
        _clients.Setup(c => c.Group($"bid-{bidId}")).Returns(_groupClientProxy.Object);

        var hub = CreateHub(GetUserPrincipal());

        // Act
        await hub.DeleteMessage(messageId, 1);

        // Assert
        _chatService.Verify(s => s.Delete(messageId), Times.Once);
        _groupClientProxy.Verify(c =>
            c.SendAsync(
                "MessageDeleted",
                It.Is<object?[]>(args => args.Length == 1 && (int)args[0] == messageId),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}