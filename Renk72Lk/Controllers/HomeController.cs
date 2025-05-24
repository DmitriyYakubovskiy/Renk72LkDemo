using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renk72Lk.DataAccess.Enums;
using Renk72Lk.Models;
using Renk72Lk.Models.DataBase;
using Renk72Lk.Services.DataBase;

namespace Renk72Lk.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize("NotBannedPolicy")]
public class HomeController : Controller
{
    private readonly IUserService userService;
    private readonly IBidPersonalInfoService bid1Service;
    private readonly IBidRepresentativeInfoService bid2Service;

    public HomeController(IUserService userService, IBidPersonalInfoService bid1Service, IBidRepresentativeInfoService bid2Service)
    {
        this.userService = userService;
        this.bid1Service = bid1Service;
        this.bid2Service = bid2Service;
    }

    [HttpGet("")]
    [HttpGet("/")]
    public IActionResult Index()
    {
        return Redirect("/Home/Profile");
    }

    [HttpGet("PrivacyPolicy")]
    [AllowAnonymous]
    public IActionResult PrivacyPolicy()
    {
        return View();
    }

    [HttpGet("TermsOfUse")]
    [AllowAnonymous]
    public IActionResult TermsOfUse()
    {
        return View();
    }

    [HttpGet("Profile")]
    public async Task<IActionResult> Profile()
    {
        var model = await userService.GetByUserNameAsync(User.Identity?.Name!, true, true);
        return View(model);
    }

    [HttpPost("UpdateProfile")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProfile([FromForm] UserModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        if (model.UserName != user?.UserName) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));
        if (model.Email != user?.Email) return BadRequest(ResultModel.GetErrors(["Почту сменить нельзя"]));
        if (model.Id != user.Id) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));

        var response = await userService.UpdateAsync(model!);

        if (response.Success) return Ok();

        return BadRequest(new { errors = response.Errors });
    }

    [HttpGet("Representative")]
    public async Task<IActionResult> Representative()
    {
        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        var bid2 = bid2Service.GetRepresentative(user.Id);
        if (bid2 != null) bid2.Role = UserType.NaturalPerson.GetDescription();
        return View(bid2);
    }

    [HttpPost("Representative")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateRepresentative([FromForm] BidRepresentativeInfoModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = await userService.GetByUserNameAsync(User?.Identity?.Name!);
        if (model.UserId != user.Id) return BadRequest(ResultModel.GetErrors(["Нет доступа"]));

        await bid2Service.CreateAsync(model);
        return Ok();
    }
}
