using Microsoft.AspNetCore.Mvc.Filters;

namespace Diplom_Project
{
    public class LogFilter : Attribute, IActionFilter
    {
        public LogFilter(ILogger<LogFilter> logger)
        {
            Logger = logger;
        }

        public ILogger Logger { get; private set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Logger.LogInformation($"{context.HttpContext.Request.Path}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"{context.HttpContext.Request.Path}");
        }
    }
}
