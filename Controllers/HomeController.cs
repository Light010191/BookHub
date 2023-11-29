using BooksHub.Filtres;
using BooksHub.Models;
using BooksHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BooksHub.Controllers
{
	public class HomeController : Controller
	{
        private readonly ILogger<HomeController> _logger;
        private readonly IUserManager _userManager;

        public HomeController(ILogger<HomeController> logger, IUserManager userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (_userManager.CurrentUser == null)
            {
                Console.WriteLine("CurrentUser NULL");
            }
            else
            {
                Console.WriteLine($" {_userManager.CurrentUser.Login} = LOGIN of CurrUser");
            }

            ViewBag.UserName = _userManager.CurrentUser?.Login ?? "Guest";

            return View();
        }


        
        [AuthorizeFilter]

        [Authorize]
        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}