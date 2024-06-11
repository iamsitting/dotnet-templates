using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TemplateProject.WebApi.Htmx;

[TypeFilter(typeof(HtmxExceptionFilter))]

public abstract class HtmxPageModel : PageModel
{
    public bool IsHtmxRequest => HttpContext.Request.Headers["HX-Request"].Any();
    public bool IsLoggedIn => User.Identity?.IsAuthenticated ?? false;
    public string? CurrentUrl => HttpContext.Request.Headers["HX-Current-URL"].FirstOrDefault();
    public string PageTitle { get; set; } = "";
    
    /// <summary>
    /// Boosted is wrapper around Page. If it's triggered by an HX-Request it acts as a boost
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public IActionResult BoostedPage(string target = "#main-content")
    {
        if (IsHtmxRequest)
        {
            HttpContext.Response.Headers["HX-Retarget"] = target;
            HttpContext.Response.Headers["HX-Reselect"] = target;
            HttpContext.Response.Headers["HX-Push-Url"] = HttpContext.Request.Path.ToString();
        }

        return Page();
    }

    public IActionResult HxRedirect(string url)
    {
        HttpContext.Response.Headers["HX-Redirect"] = url;
        return new ContentResult
        {
            Content = null,
            ContentType = null,
            StatusCode = 302
        };
    }
    public IActionResult BoostedRedirect(string url)
    {
        return IsHtmxRequest
            ? HxRedirect(url)
            : Redirect(url);
    }

    public IActionResult ForbiddenContent(string message)
    {
        return new ContentResult
        {
            StatusCode = 403,
            Content = AlertContent(message, "alert-warning"),
            ContentType = "text/html",
        };
    }


    public IActionResult ErrorContent(string message)
    {
        return new ContentResult
        {
            StatusCode = 400,
            Content = AlertContent(message),
            ContentType = "text/html",
        };
    }

    private static string AlertContent(string message, string type = "alert-danger") => $"""
                                                                 <div class="alert {type} alert-dismissible fade show" role="alert">
                                                                     {message}
                                                                     <button type="button" class="btn-close" aria-label="Close" onclick="event.target.parentElement.remove()"></button>
                                                                     </div>
                                                                 """;
}