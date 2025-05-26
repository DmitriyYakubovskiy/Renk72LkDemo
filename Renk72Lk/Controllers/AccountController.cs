using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using Renk72Lk.Helpers;
using Renk72Lk.Models;
using Renk72Lk.Services;
using Renk72Lk.Services.DataBase;
using Renk72Lk.Settings;

namespace Renk72Lk.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly IUserService userService;
    private readonly IEmailSerivce emailService;

    public AccountController(IUserService userService, IEmailSerivce emailService)
    {
        this.userService = userService;
        this.emailService = emailService;
    }

    [HttpGet("Login")]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl = "/")
    {
        LoginModel model = new LoginModel() { ReturnUrl=returnUrl};
        return View("Login", model);
    }


    [HttpPost("Login")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].ToString().Split(',')[0];
        }

        var response = await userService.LoginAsync(model, ipAddress!);
        if (!response.Success)
        {
            return BadRequest(new {errors = response.Errors});
        }
        return Ok();
    }

    [HttpGet("Register")]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View("Register", new RegistrationModel());
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromForm] RegistrationModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].ToString().Split(',')[0];
            }
            var response = await userService.RegisterUserAsync(model, ipAddress!);

            if (response.Success)
            {
                await emailService.NotifyAdminAboutRegistration(MetadataProvider, ModelState, model, Url.Action("Login", "Account", new { }, Request.Scheme));
                await emailService.NotifyUserAboutRegistration(MetadataProvider, ModelState, model, Url.Action("Login", "Account", new { }, Request.Scheme));

                return Ok();
            }
            else
            {
                return BadRequest(new { errors = response.Errors });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ResultModel.GetErrors([$"Ошибка: {ex.Message}"]));
        }
    }

    [HttpGet("Logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await userService.LogOutAsync()!;
        return Redirect("/");
    }

    [HttpGet("ForgotPassword")]
    [AllowAnonymous]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost("ForgotPassword")]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromForm] string email)
    {
        try
        {
            var user = await userService.GetByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(ResultModel.GetErrors(["Такой почты не зарегистрировано"], "email"));
            }

            var token = await userService.GeneratePasswordResetTokenAsync(user.Id);
            var resetLink = Url.Action("ResetPassword", "Account", new { token = token }, Request.Scheme);

            await emailService.SendResetPasswordToken(MetadataProvider, ModelState, user, resetLink!);

            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ResultModel.GetErrors([$"Ошибка: {ex.Message}"]));
        }
    }

    [HttpGet("ResetPassword")]
    [AllowAnonymous]
    public IActionResult ResetPassword(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest(ResultModel.GetErrors(["Некорректный токен"]));
        }

        return View(new PasswordResetModel { Token = token });
    }

    [HttpPost("ResetPassword")]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromForm] PasswordResetModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await userService.ResetPasswordAsync(model.Email, model.Token, model.Password);

            if (response.Success)
            {
                return Ok();
            }
            return BadRequest(new { errors = response.Errors });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ResultModel.GetErrors([$"Ошибка: {ex.Message}"]));
        }
    }

    [HttpGet("SendTestMessage")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SendTestMessage()
    {
        var url = Url.Action("Login", "Account", new { }, Request.Scheme);
        await emailService.Test(MetadataProvider, ModelState, url!);

        return Redirect("/");
    }

    [HttpGet("AccessDenied")]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
