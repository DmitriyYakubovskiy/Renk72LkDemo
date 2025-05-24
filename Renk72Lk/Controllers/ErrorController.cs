using Microsoft.AspNetCore.Mvc;

namespace Renk72Lk.Controllers;

[Route("[controller]")]
[ApiController]
public class ErrorController : Controller
{
    [HttpGet("404")]
    public ActionResult NotFound()
    {
        Response.StatusCode = 404;
        return View();
    }

    [HttpGet("{id}")]
    public ActionResult Errors(int id)
    {
        Response.StatusCode = id;
        return View(id);
    }
}
