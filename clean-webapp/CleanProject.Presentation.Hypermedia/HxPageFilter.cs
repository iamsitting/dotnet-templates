using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CleanProject.Presentation.Hypermedia;

public class HxPageFilter : IExceptionFilter
{
    private readonly IHostEnvironment _hostEnvironment;

    private readonly ILogger<HxPageFilter> _logger;

    public HxPageFilter(ILogger<HxPageFilter> logger, IHostEnvironment hostEnvironment)
    {
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }

    public void OnException(ExceptionContext context)
    {
        var showDetail = _hostEnvironment.IsEnvironment("Development") || _hostEnvironment.IsEnvironment("Testing") || _hostEnvironment.IsEnvironment("Preview");
        var userName = context.HttpContext.User.Identity?.Name ?? "Anonymous";
        _logger.LogError("{id} at [{method}: {path} - {query}]: {error}", userName, context.HttpContext.Request.Method, context.HttpContext.Request.Path, context.HttpContext.Request.QueryString, context.Exception);

        context.Result = context.Exception switch
        {
            AccessControlException => new ContentResult()
            {
                StatusCode = 403,
                Content = FormatContent(showDetail ? context.Exception.ToString() : "Forbidden", "warning"),
                ContentType = "text/html"
            },
            _ => new ContentResult()
            {
                StatusCode = 500,
                Content = FormatContent(showDetail ? context.Exception.ToString() : "Server Error"),
                ContentType = "text/html"
            }
        };
    }

    private static string FormatContent(string message, string type = "alert-danger")
    {
        return $"""
                <div class="alert {type} alert-dismissible fade show" role="alert">
                    {HttpUtility.HtmlEncode(message)}
                    <button type="button" class="btn-close" aria-label="Close" onclick="event.target.parentElement.remove()"></button>
                    </div>
                """;
    }
}

public class AccessControlException : Exception
{
    public AccessControlException(string message)
        : base(message)
    {
    }
}