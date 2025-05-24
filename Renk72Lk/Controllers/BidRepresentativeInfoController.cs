using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renk72Lk.Models;
using Renk72Lk.Models.DataBase;
using Renk72Lk.Services;
using Renk72Lk.Services.DataBase;

namespace Renk72Lk.Controllers;

[Route("Bid")]
[ApiController]
[Authorize("NotBannedPolicy")]
public class BidRepresentativeInfoController : Controller
{
    private readonly IUserService userService;
    private readonly IBidPersonalInfoService bid1Service;
    private readonly IBidRepresentativeInfoService bid2Service;
    private readonly IBidViewModelService bidViewModelService;

    public BidRepresentativeInfoController(IUserService userService, IBidPersonalInfoService bid1Service, IBidRepresentativeInfoService bid2Service, IBidViewModelService bidViewModelService)
    {
        this.userService = userService;
        this.bid1Service = bid1Service;
        this.bid2Service = bid2Service;
        this.bidViewModelService = bidViewModelService;
    }

    [HttpGet("Bid2")]
    public async Task<IActionResult> Bid2()
    {
        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        var viewBid = bidViewModelService.GetCreateBidViewModelByUserId(user.Id);

        if (viewBid.Bid.Step1 == null || viewBid.Bid.Step2 == null || new[] { 1 }.Contains(BidViewModelService.Validate(viewBid))) return Redirect("/Bid/Bid1");

        viewBid.IsRepresentativeData = bid2Service.GetRepresentative(user.Id) != null;

        return View("~/Views/Bid/Bid.cshtml", viewBid);
    }

    [HttpPost("Bid2")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Bid2([FromForm] BidPersonalInfoModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        var viewBid = bidViewModelService.GetCreateBidViewModelByUserId(user.Id);

        if(user.Id != model.UserId) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
        if (viewBid.Bid.Id != model.BidId) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
        if (viewBid.Bid.Step1 == null)
        {
            await bid1Service.CreateAsync(model);
        }
        else
        {
            if(viewBid.Bid.Step1.Id != model.Id) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
            await bid1Service.UpdateAsync(model);
        }

        viewBid.Bid.Step1 = bid1Service.GetOne(user.Id, viewBid.Bid.Id);

        if (viewBid.Bid.Step2 == null)
        {
            await bid2Service.CreateAsync(new BidRepresentativeInfoModel() { UserId = user.Id, BidId = viewBid.Bid.Id });
            viewBid.Bid.Step2 = bid2Service.GetOne(user.Id, viewBid.Bid.Id);
        }
        viewBid.Bid.Step2.Role = viewBid.Bid.UserRole;

        viewBid.IsRepresentativeData = bid2Service.GetRepresentative(user.Id) != null;

        return View("~/Views/Bid/Bid.cshtml", viewBid);
    }

    [HttpGet("GetDataRepresentative")]
    public async Task<IActionResult> GetDataRepresentative()
    {
        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        var bid2 = bid2Service.GetRepresentative(user.Id);
        bid2.User = null!;
        return Json(bid2);
    }
}
