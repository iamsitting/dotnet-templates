using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TemplateProject.WebApi.Htmx;

public class HtmxExceptionFilter : IExceptionFilter
{
    private readonly IHostEnvironment _hostEnvironment;

    private readonly ILogger<HtmxExceptionFilter> _logger;

    public HtmxExceptionFilter(ILogger<HtmxExceptionFilter> logger, IHostEnvironment hostEnvironment)
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
                Content = FormatContent(showDetail ? context.Exception.ToString() : "Server Error", "danger"),
                ContentType = "text/html"
            }
        };
    }

    private static string FormatContent(string message, string type = "danger")
    {
        return $"<div class=\"uk-alert-{type}\" uk-alert><a href class=\"uk-alert-close\" uk-close></a><div>{message}</div></div>";
    }
}

public class AccessControlException : Exception
{
    public AccessControlException(string message)
        : base(message)
    {
    }
}