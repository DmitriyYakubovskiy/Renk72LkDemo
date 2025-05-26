using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renk72Lk.DataAccess.Enums;
using Renk72Lk.DataAccess.Extensions;
using Renk72Lk.Models.DataBase;
using Renk72Lk.Services;
using Renk72Lk.Services.DataBase;

namespace Renk72Lk.Controllers;

[Route("Bid")]
[ApiController]
[Authorize("NotBannedPolicy")]
public class BidPersonalInfoController : Controller
{
    private readonly IUserService userService;
    private readonly IBidService bidService;
    private readonly IBidPersonalInfoService bid1Service;
    private readonly IBidViewModelService bidViewModelService;

    public BidPersonalInfoController(IUserService userService, IBidService bidService, IBidPersonalInfoService bid1Service, IBidViewModelService bidViewModelService)
    {
        this.userService = userService;
        this.bidService = bidService;
        this.bid1Service = bid1Service;
        this.bidViewModelService = bidViewModelService;
    }

    [HttpGet("Bid1")]
    public async Task<IActionResult> Bid1()
    {
        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        var viewBid = bidViewModelService.GetCreateBidViewModelByUserId(user.Id);

        if (viewBid.Bid == null || viewBid.Bid.Step1 == null) return Redirect("/");

        viewBid.IsConsumerData = bid1Service.GetConsumer(user.Id) != null;

        return View("~/Views/Bid/Bid.cshtml", viewBid);
    }

    [HttpPost("Bid1")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Bid1([FromForm] string service)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        DateTime oneDayAgo = DateTime.UtcNow.AddDays(-1);
        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        var role = await userService.GetUserClaimValueAsync(user.UserName!, UserType.UserType.GetDescription());

        var userBid = bidService.GetOne(user.Id, 0, oneDayAgo);

        if (userBid == null)
        {
            var newUserBid = new BidModel
            {
                Service = service,
                UserId = user.Id,
                Status = 0,
                UserRole = role
            };
            await bidService.CreateAsync(newUserBid);
        }
        else
        {
            userBid.Service = service;
            bidService.Update(userBid);
        }

        var viewBid = bidViewModelService.GetCreateBidViewModelByUserId(user.Id);
        if (viewBid.Bid.Step1 == null)
        {
            await bid1Service.CreateAsync(new BidPersonalInfoModel() { UserId = user.Id, BidId = viewBid.Bid.Id });
            viewBid.Bid.Step1 = bid1Service.GetOne(user.Id, viewBid.Bid.Id);
        }

        viewBid.Bid.Step1.Role = role;
        viewBid.IsConsumerData = bid1Service.GetConsumer(user.Id) != null;

        return View("~/Views/Bid/Bid.cshtml", viewBid);
    }

    [HttpGet("GetDataProfile")]
    public async Task<IActionResult> GetDataProfile()
    {
        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        return Json(user);
    }

    [HttpGet("GetDataConsumer")]
    public async Task<IActionResult> GetDataConsumer()
    {
        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        var bid1 = bid1Service.GetConsumer(user.Id);
        bid1.User=null!;
        return Json(bid1);
    }
}
