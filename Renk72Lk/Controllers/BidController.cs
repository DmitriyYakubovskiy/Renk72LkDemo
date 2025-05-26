using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Renk72Lk.DataAccess.Enums;
using Renk72Lk.DataAccess.Extensions;
using Renk72Lk.Helpers;
using Renk72Lk.Models;
using Renk72Lk.Models.DataBase;
using Renk72Lk.Services;
using Renk72Lk.Services.DataBase;
using Renk72Lk.Settings;
using System.Data;
using System.Globalization;

namespace Renk72Lk.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize("NotBannedPolicy")]
public class BidController : Controller
{
    private readonly IUserService userService;
    private readonly IBidService bidService;
    private readonly IBidAttachmentsService bid5Service;
    private readonly IFileService fileService;
    private readonly IEmailSerivce emailService;
    private readonly IBidViewModelService bidViewModelService;

    private readonly HttpClient httpClient;
    private readonly ReportingSettings apiSettings;

    public BidController(IUserService userService, IBidService bidService, IBidAttachmentsService bid5Service, 
        IFileService fileService, IEmailSerivce emailService,
        IBidViewModelService bidViewModelService, HttpClient httpClient, IOptions<ReportingSettings> apiSettings)
    {
        this.userService = userService;
        this.bidService = bidService;
        this.bid5Service = bid5Service;

        this.fileService = fileService;
        this.emailService = emailService;

        this.bidViewModelService = bidViewModelService;
        this.httpClient = httpClient;
        this.apiSettings = apiSettings.Value;
    }

    [HttpGet("")]
    public async Task<IActionResult> List(string? service = "", string? role = "", string? ticketStatus = null, string? surname = "", string? date = null,
        int? take = 100, int? skip = 0)
    {
        if (User.IsInRole(UserRole.Admin.GetDescription()))
        {
            ViewData["Skip"] = skip;
            return base.View("List", await GetBids(isArchive: 0, service: service, role: role, ticketStatuses: [ticketStatus!], surname: surname, date: date, take: take, skip: skip));
        }

        return View("List", await GetBids(service: service, role:role, ticketStatuses: [ticketStatus!], surname: surname, date: date, take: null, skip: null));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("Archives")]
    public async Task<IActionResult> ListArchives(string? service = "", string? role = "", string? date = null)
    {
        return View("~/Views/Bid/Admin/ListArchives.cshtml", await GetBids(isArchive:1, service: service, role: role, date:date));
    }

    [HttpGet("LoadNext")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoadNext(string? service = "", string? role = "", string? ticketStatus = null, string? surname = "", string? date = null, int? take = null, int? skip =null)
    {
        return PartialView("_PartialRequestList", await GetBids(isArchive: 0, service: service, role: role, ticketStatuses: [ticketStatus!], surname: surname, date: date, take: take, skip: skip));
    }

    private async Task<BidModel[]> GetBids(int? isArchive = null, string?
    department = null, string? service = null, string? role = null, string[] ticketStatuses = null!, string? surname = null, string? date = null,
    int? take = null, int? skip = null)
    {
        List<int?> ticketStatusesInt = new List<int?>();
        if (ticketStatuses != null)
        {
            foreach (var ticketStatus in ticketStatuses)
            {
                if (ticketStatus == "Все")
                {
                    ticketStatusesInt = null!;
                    break;
                }
                if (ticketStatus != null)
                {
                    ticketStatusesInt.Add(TicketStatus.ticketStatuses.Where(x => x.Name == ticketStatus).FirstOrDefault()?.Id);
                }
            }
        }
        if (service == "Все") service = null;
        if (role == "Все") role = null;

        var user = await userService.GetByUserNameAsync(User.Identity?.Name!);
        DateTime? dateTime = null;

        if (!string.IsNullOrEmpty(date))
        {
            if (DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate)) dateTime = parsedDate;
            else return null!;
        }

        if (User.IsInRole(UserRole.Admin.GetDescription())) user.Bids = bidService.GetAll(status: 1, isArchive: isArchive, department: department, service: service, role: role, ticketStatuses: ticketStatusesInt,
            surname: surname, date: dateTime, includeUser: true, take: take, skip: skip).ToList();
        else user.Bids = bidService.GetAll(status: 1, userId: user.Id, service: service, date: dateTime, includeUser: true).ToList();

        return user.Bids.ToArray();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] BidAttachmentsModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        var viewBid = bidViewModelService.GetCreateBidViewModelByUserId(user.Id);

        if (user.Id != model.UserId) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
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
        viewBid = bidViewModelService.GetCreateBidViewModelByUserId(user.Id);

        int docId = 0;
        try
        {
            docId = await fileService.CreateBidDocumentFileAsync(viewBid);
            if (docId == -1) return BadRequest(ResultModel.GetErrors(["Ошибка при генерации PDF"]));
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ResultModel.GetErrors([$"Ошибка при генерации PDF: {ex.Message}"]));
        }

        viewBid.Bid.DocumentFileId = docId;
        viewBid.Bid.Status = 1;
        bidService.Update(viewBid.Bid);

        var bidsUrl = Url.Action("List", "Bid", new { }, Request.Scheme);
        var documentUrl = $"{Request.Scheme}://{Request.Host}/{viewBid.Bid?.DocumentFile?.FilePath}";

        await emailService.NotifyUserAboutCreationBid(MetadataProvider, ModelState, user, viewBid.Bid.Id, bidsUrl!, documentUrl);
        await emailService.NotifyAdminAboutCreationBid(MetadataProvider, ModelState, user, viewBid.Bid.Id, bidsUrl!, documentUrl);

        return RedirectToAction("List", "Bid");
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        var bid = bidService.GetById(id, includeUser: true, includeBid1: true, includeBid2: true, includeBid5: true, includeTickets:true);

        if (bid == null || bid.Status==0) return BadRequest(ResultModel.GetErrors(["Заявка не найдена" ]));
        if (user.Id != bid.UserId && !(await userService.GetUserRolesAsync(user.Id)).Contains(UserRole.Admin.GetDescription())) return base.BadRequest(ResultModel.GetErrors(["Нет доступа к данной заявке"]));

        return View("Details", (bid, user));
    }

    [HttpPost("TicketStatus")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeTicketStatus([FromForm] int id, [FromForm] int ticketStatus)
    {
        var bid = bidService.GetById(id);
        var user = await userService.GetByIdAsync(bid.UserId.Value);

        if (bid == null) return BadRequest(ResultModel.GetErrors(["Заявка не найдена"]));
            
        bid.TicketStatus = ticketStatus;
            
        bidService.Update(bid);

        await emailService.NotifyUserAboutNewBidStatus(MetadataProvider, ModelState, user, bid.Id, TicketStatus.ticketStatuses.Where(x => x.Id == bid.TicketStatus).FirstOrDefault()?.Name!, $"{Request.Scheme}://{Request.Host}/{bid?.DocumentFile?.FilePath}");

        return Ok();
    }

    [HttpPost("ToArchive")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public IActionResult ToArchive([FromForm] int id)
    {
        var bid = bidService.GetById(id);

        if (bid != null)
        {
            bid.IsArchive = 1;
            bidService.Update(bid);
            return Ok();
        }

        return BadRequest(ResultModel.GetErrors(["Заявка не найдена"]));
    }

    [HttpPost("FromArchive")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public IActionResult FromArchive([FromForm] int id)
    {
        var bid = bidService.GetById(id);

        if (bid != null)
        {
            bid.IsArchive = 0;
            bidService.Update(bid);
            return Ok();
        }

        return BadRequest(ResultModel.GetErrors(["Заявка не найдена"]));
    }

    [HttpGet("DopInfo")]
    public async Task<IActionResult> DopInfo(int id)
    {
        try
        {
            var currentUser = await userService.GetByUserNameAsync(User.Identity!.Name!);
            var bid = bidService.GetById(id, includeBid1: true);

            if (currentUser.Id != bid.UserId && !(await userService.GetUserRolesAsync(currentUser.Id)).Contains(UserRole.Admin.GetDescription())) return base.BadRequest(ResultModel.GetErrors(["Нет доступа к данной заявке"]));

            var response = await httpClient.PostAsJsonAsync($"http://{apiSettings.Host}:{apiSettings.Port}/api/generate-dopinfo", bid.Step1);

            if (response.IsSuccessStatusCode)
            {
                var pdfBytes = await response.Content.ReadAsByteArrayAsync();
                return File(pdfBytes.ToArray(), "application/pdf");
            }

            var errorMessage = await response.Content.ReadAsStringAsync();

            return BadRequest(ResultModel.GetErrors([$"Ошибка при создании PDF: {errorMessage} {response.StatusCode}"]));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ResultModel.GetErrors([$"Ошибка при создании PDF: {ex.Message}"]));
        }
    }
}
