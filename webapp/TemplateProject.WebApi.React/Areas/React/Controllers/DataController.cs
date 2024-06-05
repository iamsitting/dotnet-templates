using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.WebApi.React.Areas.React.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DataController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}