using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Presentation.React.Areas;

[Route("api/_react/[controller]/")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public abstract class AreaControllerBase : ControllerBase
{
    [HttpGet("_test")]
    [AllowAnonymous]
    public IActionResult Test()
    {
        return Ok("success");
    }
}