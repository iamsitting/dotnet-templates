using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.WebApi.React.Areas.React;

[Area("React")]
[Route("[area]/api/[controller]/")]
[ApiController]
public abstract class AreaControllerBase : ControllerBase
{
    [HttpGet("_test")]
    [AllowAnonymous]
    public IActionResult Test()
    {
        return Ok("success");
    }
}