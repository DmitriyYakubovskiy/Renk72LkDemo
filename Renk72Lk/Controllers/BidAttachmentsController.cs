using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Renk72Lk.DataAccess.Enums;
using Renk72Lk.Models;
using Renk72Lk.Models.DataBase;
using Renk72Lk.Services;
using Renk72Lk.Services.DataBase;
using Renk72Lk.Settings;

namespace Renk72Lk.Controllers;

[Route("Bid")]
[ApiController]
[Authorize("NotBannedPolicy")]
public class BidAttachmentsController : Controller
{
    private readonly IUserService userService;
    private readonly IBidTechnicalSpecificationsService bid4Service;
    private readonly IBidAttachmentsService bid5Service;
    private readonly IBidViewModelService bidViewModelService;
    private readonly IAttachmentFileService fileService;

    private readonly HttpClient httpClient;
    private readonly PdfGeneratorApiSettings apiSettings;

    public BidAttachmentsController(IUserService userService, IBidTechnicalSpecificationsService bid4Service, IBidAttachmentsService bid5Service,
        IAttachmentFileService fileService, IBidViewModelService bidViewModelService, HttpClient httpClient, 
        IOptions<PdfGeneratorApiSettings> apiSettings
        )
    {
        this.userService = userService;
        this.bid4Service = bid4Service;
        this.bid5Service = bid5Service;
        this.bidViewModelService = bidViewModelService;
        this.fileService = fileService;
        this.httpClient = httpClient;
        this.apiSettings = apiSettings.Value;
    }

    [HttpGet("Bid5")]
    public async Task<IActionResult> Bid5()
    {
        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        var viewBid = bidViewModelService.GetCreateBidViewModelByUserId(user.Id);

        if (viewBid.Bid.Step4 == null || viewBid.Bid.Step5 == null || new[] { 1,2,3,4 }.Contains(BidViewModelService.Validate(viewBid))) return Redirect("/Bid/Bid4");

        return View("~/Views/Bid/Bid.cshtml", viewBid);
    }

    [HttpPost("Bid5")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Bid5([FromForm] BidTechnicalSpecificationsModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        var viewBid = bidViewModelService.GetCreateBidViewModelByUserId(user.Id);

        if (user.Id != model.UserId) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
        if (viewBid.Bid.Id != model.BidId) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
        if (viewBid.Bid.Step4 == null)
        {
            await bid4Service.CreateAsync(model);
        }
        else
        {
            if (viewBid.Bid.Step4.Id != model.Id) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
            bid4Service.Update(model);
        }

        if (viewBid.Bid.Step4 == null) await bid4Service.CreateAsync(model);
        else bid4Service.Update(model);

        viewBid.Bid.Step4 = bid4Service.GetOne(user.Id, viewBid.Bid.Id);

        if (viewBid.Bid.Step5 == null)
        {
            await bid5Service.CreateAsync(new BidAttachmentsModel() { UserId = user.Id, BidId = viewBid.Bid.Id });
            viewBid.Bid.Step5 = bid5Service.GetOne(user.Id, viewBid.Bid.Id);
        }
        viewBid.Bid.Step5.Role = viewBid.Bid.UserRole;
        if (!string.IsNullOrEmpty(viewBid.Bid.Step2?.Name) && viewBid.Bid.UserRole == "Юридическое лицо")
        {
            viewBid.Bid.Step5.IsAttorney = "on";
        }

        return View("~/Views/Bid/Bid.cshtml", viewBid);
    }

    [HttpPost("SaveBid5ForPrePdf")]
    public async Task<IActionResult> SaveBid5ForPrePdf(IFormCollection collection)
    {
        var user = await userService.GetByUserNameAsync(User.Identity.Name);
        var model = new BidAttachmentsModel();

        model.Id = int.Parse(collection["Id"]);
        model.UserId = int.Parse(collection["UserId"]);
        model.BidId = int.Parse(collection["BidId"]);
        List<IFormFile> otherFiles = new List<IFormFile>();
        List<IFormFile> passportFiles = new List<IFormFile>();
        List<IFormFile> snilsFiles = new List<IFormFile>();
        List<IFormFile> planFiles = new List<IFormFile>();
        List<IFormFile> benefitFiles = new List<IFormFile>();

        if (user.Id != model.UserId) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));

        if (collection.Files.Count > 0)
        {
            foreach (var file in collection.Files)
            {
                if (file.Length == 0 || file.FileName.Length == 0) continue;
                switch (file.Name)
                {
                    case "PassportFiles":
                        passportFiles.Add(file);
                        break;

                    case "PlanFiles":
                        planFiles.Add(file);
                        break;

                    case "SnilsFiles":
                        snilsFiles.Add(file);
                        break;

                    case "BenefitFiles":
                        benefitFiles.Add(file);
                        break;

                    case "OtherFiles":
                        otherFiles.Add(file);
                        break;
                }
            }
        }

        var viewBid = bidViewModelService.GetCreateBidViewModelByUserId(user.Id);
        model.OtherFiles = otherFiles.ToArray();
        model.PassportFiles = passportFiles.ToArray();
        model.SnilsFiles = snilsFiles.ToArray();
        model.BenefitFiles = benefitFiles.ToArray();
        model.PlanFiles = planFiles.ToArray();

        if (viewBid.Bid.Id != model.BidId) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
        if (viewBid.Bid.Step5 == null)
        {
            await bid5Service.CreateAsync(model);
        }
        else
        {
            if (viewBid.Bid.Step5.Id != model.Id) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
            await bid5Service.UpdateAsync(model);
        }

        var url = Url.Action("PredPDF", new { id = viewBid.Bid.Id });

        return Json(new { url = url });
    }

    [HttpGet("PredPDF")]
    public async Task<IActionResult> PredPDF(int id)
    {
        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        var viewBid = bidViewModelService.GetCreateBidViewModelById(id);

        if (user.Id != viewBid.Bid.UserId && !(await userService.GetUserRolesAsync(user.Id)).Contains(UserRole.Admin.GetDescription())) return BadRequest(ResultModel.GetErrors(["Нет доступа к данной заявке"]));

        var response = await httpClient.PostAsJsonAsync($"http://{apiSettings.Host}:{apiSettings.Port}/api/generate-pdf", viewBid.Bid);
        
        if (response.IsSuccessStatusCode)
        {
            var pdfBytes = await response.Content.ReadAsByteArrayAsync();
            return File(pdfBytes.ToArray(), "application/pdf");
        }

        var errorMessage = await response.Content.ReadAsStringAsync();
        return BadRequest($"Ошибка при создании PDF: {errorMessage} {response.StatusCode}");
    }

    [HttpPost("RemoveUploadedFile")]
    public async Task<IActionResult> RemoveUploadedFile([FromForm] string fieldType, [FromForm] string bidId)
    {
        try
        {
            var user = await userService.GetByUserNameAsync(User.Identity.Name);
            var viewBid = bidViewModelService.GetCreateBidViewModelById(Convert.ToInt32(bidId));

            if (user.Id != viewBid.Bid.UserId) return BadRequest(ResultModel.GetErrors(["Нет доступа к данной заявке"]));

            switch (fieldType)
            {
                case "passportFile":
                    viewBid.Bid.Step5.PassportFileId = null;
                    break;

                case "planFile":
                    viewBid.Bid.Step5.PowerDevicesPlanFileId = null;
                    break;

                case "snilsFile":
                    viewBid.Bid.Step5.SnilsFileId = null;
                    break;

                case "benefitFile":
                    viewBid.Bid.Step5.BenefitFileId = null;
                    break;

                case "otherFile":
                    viewBid.Bid.Step5.OtherFileId = null;
                    break;
            }
            await bid5Service.UpdateAsync(viewBid.Bid.Step5);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ResultModel.GetErrors([ex.Message]));
        }

        return Ok();
    }
}
