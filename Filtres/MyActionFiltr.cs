using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BooksHub.Services;

namespace BooksHub.Filtres
{
    public class MyActionFiltr : Attribute, IActionFilter
    {
       
        public string Message { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Cookies.Append("test", DateTime.Now.ToString());                      
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is Controller controller)
            {
                controller.ViewBag.FilterData = Message;
                context.HttpContext.Response.Cookies.Append("filter", DateTime.Now.ToString());
            }

        }
    }
}
