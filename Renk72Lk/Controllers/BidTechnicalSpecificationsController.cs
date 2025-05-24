using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renk72Lk.Models;
using Renk72Lk.Models.DataBase;
using Renk72Lk.Services;
using Renk72Lk.Services.DataBase;
using System.Globalization;

namespace Renk72Lk.Controllers;

[Route("Bid")]
[ApiController]
[Authorize("NotBannedPolicy")]
public class BidTechnicalSpecificationsController : Controller
{
    private readonly IUserService userService;
    private readonly IBidService bidService;
    private readonly IBidConnectionObjectInfoService bid3Service;
    private readonly IBidTechnicalSpecificationsService bid4Service;
    private readonly IAttachmentsPointService pointService;
    private readonly IAttachmentsStageService stageService;
    private readonly IBidViewModelService bidViewModelService;

    public BidTechnicalSpecificationsController(IUserService userService, IBidService bidService, IBidConnectionObjectInfoService bid3Service, 
        IBidTechnicalSpecificationsService bid4Service, IAttachmentsPointService pointService, IAttachmentsStageService stageService, 
        IBidViewModelService bidViewModelService)
    {
        this.userService = userService;
        this.bidService = bidService;
        this.bid3Service = bid3Service;
        this.bid4Service = bid4Service;
        this.pointService = pointService;
        this.stageService = stageService;
        this.bidViewModelService = bidViewModelService;
    }

    [HttpGet("Bid4")]
    public async Task<IActionResult> Bid4()
    {
        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        var viewBid = bidViewModelService.GetCreateBidViewModelByUserId(user.Id);

        if (viewBid.Bid.Step3 == null || viewBid.Bid.Step4 == null || new[] { 1, 2, 3 }.Contains(BidViewModelService.Validate(viewBid))) return Redirect("/Bid/Bid3");

        return View("~/Views/Bid/Bid.cshtml", viewBid);
    }

    [HttpPost("Bid4")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Bid4([FromForm] BidConnectionObjectInfoModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
            var viewBid = bidViewModelService.GetCreateBidViewModelByUserId(user.Id);

            if (user.Id != model.UserId) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
            if (viewBid.Bid.Id != model.BidId) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
            if (viewBid.Bid.Step3 == null)
            {
                await bid3Service.CreateAsync(model);
            }
            else
            {
                if (viewBid.Bid.Step3.Id != model.Id) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
                bid3Service.Update(model);
            }

            viewBid.Bid.Step3 = bid3Service.GetOne(user.Id, viewBid.Bid.Id);

            if (viewBid.Bid.Step4 == null)
            {
                await bid4Service.CreateAsync(new BidTechnicalSpecificationsModel() { UserId = user.Id, BidId = viewBid.Bid.Id });
                viewBid.Bid.Step4 = bid4Service.GetOne(user.Id, viewBid.Bid.Id);
            }
            viewBid.Bid.Step4.Role = viewBid.Bid.UserRole;
            viewBid.Bid.Step4.Reason = viewBid.Bid.Step3!.ReasonForBid!;
            viewBid.Bid.Step4.Service = viewBid.Bid.Service;
            viewBid.Bid.Department = viewBid.Bid.Step3!.GuarantySupplier!;
            viewBid.Bid.Name = viewBid.Bid.Step3.ReasonForBid!;
            bidService.Update(viewBid.Bid);

            return View("~/Views/Bid/Bid.cshtml", viewBid);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost("AddAttachmentPoint")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAttachmentPoint([FromForm] int id, [FromForm] string voltage, [FromForm] string power)
    {
        try
        {
            float voltageValue = float.Parse(voltage, CultureInfo.InvariantCulture);
            float powerValue = float.Parse(power, CultureInfo.InvariantCulture);
            var point = new AttachmentsPointModel
            {
                Voltage = voltageValue,
                Power = powerValue,
                TechnicalSpecificationsId = id
            };

            point.Id = await pointService.CreateAsync(point);

            return Json(new
            {
                success = true,
                point = new
                {
                    id = point.Id,
                    voltage = point.Voltage.ToString(),
                    power = point.Power.ToString()
                }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost("DeleteAttachmentPoint")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteAttachmentPoint([FromForm] int id)
    {
        pointService.Delete(id);
        return Json(new { success = true });
    }

    [HttpPost("AddAttachmentStage")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAttachmentStage([FromForm] ConstructionStageRequest request)
    {
        try
        {
            var stage = new AttachmentsStageModel
            {
                TechnicalSpecificationsId = request.Id,
                DesignPeriod = DateTime.Parse(request.DesignPeriod),
                CommissioningPeriod = DateTime.Parse(request.CommissioningPeriod),
                Power = float.Parse(request.Power, CultureInfo.InvariantCulture),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            int id = await stageService.CreateAsync(stage);

            return Json(new
            {
                success = true,
                id = id,
                designPeriod = stage.DesignPeriod?.ToString("yyyy-MM-dd"),
                commissioningPeriod = stage.CommissioningPeriod?.ToString("yyyy-MM-dd"),
                power = stage.Power
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost("DeleteAttachmentStage")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteAttachmentStage([FromForm] int id)
    {
        stageService.Delete(id);
        return Json(new { success = true });
    }

    public class ConstructionStageRequest
    {
        public int Id { get; set; }
        public string DesignPeriod { get; set; }
        public string CommissioningPeriod { get; set; }
        public string Power { get; set; }
    }
}
