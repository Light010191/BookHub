using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using BooksHub.Models;
using BooksHub.Services;
using System.Security.Policy;
using System.Text.Json;

namespace BooksHub.Middlewares
{
	public class AuthMiddleware
	{

		private RequestDelegate next;
		

		public AuthMiddleware(RequestDelegate next)
		{
			this.next = next;		
		}
		public async Task InvokeAsync(HttpContext context, IUserManager userManager)
		{
            userManager.GetUserCrededantials();
						
			await next.Invoke(context);
		}

	}
}
