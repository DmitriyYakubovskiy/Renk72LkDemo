using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Renk72Lk.DataAccess.Enums;
using Renk72Lk.DataAccess.Extensions;
using Renk72Lk.Models;
using Renk72Lk.Services.DataBase;

namespace Renk72Lk.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet("List")]
    public async Task<IActionResult> List(string sort = "date", string order = "asc")
    {
        var users = userService.GetAll().ToList();
        var userClaims = new Dictionary<int, bool>();
        foreach (var user in users)
        {
            var isBanned = await userService.GetUserClaimValueAsync(user.UserName, UserBanned.UserBanned.GetDescription()) == "true";
            userClaims[user.Id] = isBanned;
        }

        switch (sort)
        {
            case "date":
                users = order == "asc" ? users.OrderBy(u => u.CreatedAt).ToList() : users.OrderByDescending(u => u.CreatedAt).ToList();
                break;
            case "role":
                //users = order == "asc" ? users.OrderBy(u => u.Role).ToList() : users.OrderByDescending(u => u.Role).ToList();
                break;
            default:
                users = order == "asc" ? users.OrderBy(u => u.CreatedAt).ToList() : users.OrderByDescending(u => u.CreatedAt).ToList();
                break;
        }

        ViewBag.UserClaims = userClaims;
        return View(users.ToArray());
    }

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await userService.GetByIdAsync(id);
        if (user == null)
        {
            return BadRequest(ResultModel.GetErrors(["Пользователь не найден."]));
        }

        var userModel = new UpdateUserModel()
        {
            Id = user.Id,
            Name = user.Name!,
            Surname = user.Surname!,
            Patronymic = user.Patronymic!,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber!
        };

        return Ok(userModel);
    }

    [HttpPost("Update")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromForm] UpdateUserModel model)
    {
        var user = await userService.GetByIdAsync(model.Id);
        if(user == null)
        {
            return BadRequest(ResultModel.GetErrors(["Пользователь не найден."]));
        }

        user.Name = model.Name;
        user.Surname = model.Surname;
        user.Patronymic = model.Patronymic;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;
        await userService.UpdateAsync(user);

        return Ok();
    }

    [HttpPost("Lock")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Lock(int id)
    {
        var response = (await userService.LockAsync(id));
        var status = response.Item2;
        if (response.Item1.Success)
        {
            return Json(new { status = (status == 1) ? "locked" : "unlocked" });
        }
        return BadRequest(new { errors = response.Item1.Errors });
    }

    [HttpDelete("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await userService.RemoveAsync(id);

        if (result.Success) return Ok(new { id = id });
        else return BadRequest(ResultModel.GetErrors(["Ошибка удаления."]));
    }
}
