using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TemplateProject.WebApi.Htmx;

[TypeFilter(typeof(HtmxExceptionFilter))]

public abstract class HtmxPageModel : PageModel
{
    public bool IsHtmxRequest => HttpContext.Request.Headers["HX-Request"].Any();
    public bool IsLoggedIn => User.Identity?.IsAuthenticated ?? false;
    
    /// <summary>
    /// HxPage is wrapper around Page. If it's triggered by an HX-Request it acts as a boost
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    protected IActionResult HtmxPage(string target = "#main-content")
    {
        if (IsHtmxRequest)
        {
            HttpContext.Response.Headers["HX-Retarget"] = target;
            HttpContext.Response.Headers["HX-Reselect"] = target;
            HttpContext.Response.Headers["HX-Push-Url"] = HttpContext.Request.Path.ToString();
        }

        return Page();
    }

    protected NoContentResult HxRedirect(string url)
    {
        HttpContext.Response.Headers["HX-Redirect"] = url;
        return new NoContentResult();
    }
    protected IActionResult HtmxRedirect(string url)
    {
        return IsHtmxRequest
            ? HxRedirect(url)
            : Redirect(url);
    }
    
    protected static IActionResult ForbiddenContent(string message) =>
        new ContentResult
        {
            StatusCode = 403,
            Content = $"<div class=\"alert alert-warning alert-dismissible fade show\" role=\"alert\">{message}<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button></div>",
            ContentType = "text/html",
        };
    
    protected static IActionResult ErrorContent(string message) =>
        new ContentResult
        {
            StatusCode = 500,
            Content = $"<div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\">{message}<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button></div>",
            ContentType = "text/html",
        };
}