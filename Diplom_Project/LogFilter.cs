using Microsoft.AspNetCore.Mvc.Filters;

namespace Diplom_Project
{
    public class LogFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"{context.HttpContext.Request.Path}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"{context.HttpContext.Request.Path}");
        }
    }
}
