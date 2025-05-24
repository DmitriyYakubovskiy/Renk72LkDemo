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
public class BidConnectionObjectInfoController : Controller
{
    private readonly IUserService userService;
    private readonly IBidRepresentativeInfoService bid2Service;
    private readonly IBidConnectionObjectInfoService bid3Service;
    private readonly IBidViewModelService bidViewModelService;

    public BidConnectionObjectInfoController(IUserService userService, IBidRepresentativeInfoService bid2Service, IBidConnectionObjectInfoService bid3Service, IBidViewModelService bidViewModelService)
    {
        this.userService = userService;
        this.bid2Service = bid2Service;
        this.bid3Service = bid3Service;
        this.bidViewModelService = bidViewModelService;
    }

    [HttpGet("Bid3")]
    public async Task<IActionResult> Bid3()
    {
        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        var viewBid = bidViewModelService.GetCreateBidViewModelByUserId(user.Id);

        if (viewBid.Bid.Step2 == null || viewBid.Bid.Step3 == null || new[] { 1, 2 }.Contains(BidViewModelService.Validate(viewBid))) return Redirect("/Bid/Bid2");

        return View("~/Views/Bid/Bid.cshtml", viewBid);
    }

    [HttpPost("Bid3")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Bid3([FromForm] BidRepresentativeInfoModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        var viewBid = bidViewModelService.GetCreateBidViewModelByUserId(user.Id);

        if (user.Id != model.UserId) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
        if (viewBid.Bid.Id != model.BidId) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
        if (viewBid.Bid.Step2 == null)
        {
            await bid2Service.CreateAsync(model);
        }
        else
        {
            if (viewBid.Bid.Step2.Id != model.Id) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
            await bid2Service.UpdateAsync(model);
        }

        viewBid.Bid.Step2 = bid2Service.GetOne(user.Id, viewBid.Bid.Id);

        if (viewBid.Bid.Step3 == null)
        {
            await bid3Service.CreateAsync(new BidConnectionObjectInfoModel() { UserId = user.Id, BidId = viewBid.Bid.Id });
            viewBid.Bid.Step3 = bid3Service.GetOne(user.Id, viewBid.Bid.Id);
        }

        viewBid.Bid.Step3.Role = viewBid.Bid.UserRole;

        return View("~/Views/Bid/Bid.cshtml", viewBid);
    }
}
