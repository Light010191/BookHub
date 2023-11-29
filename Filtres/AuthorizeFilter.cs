using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BooksHub.Services;
using System.Security.Policy;

namespace BooksHub.Filtres
{
    public class AuthorizeFilter : Attribute, IAuthorizationFilter
    {    

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            IUserManager userManager = context.HttpContext.RequestServices.GetRequiredService<IUserManager>();

            if (userManager.CurrentUser == null)
            {
                context.Result = new RedirectToActionResult("Error", "Home", null);
            }

        }
    }
}
