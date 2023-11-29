using BooksHub.Models;
using BooksHub.Services;
using BooksHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksHub.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        private readonly UserDbContext _userDbContext;
        private readonly IUserManager _userManager;
        public AuthController(UserDbContext userDbContext, IUserManager userManager)
        {
            _userDbContext = userDbContext;
            _userManager = userManager;

        }


        public UserDbContext UserDbContext { get; }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)

        {
            if (ModelState.IsValid)
            {

                User newUser = new User()
                {
                    Login = viewModel.Login,
                    PasswordHash = SHA256Encripter.Encript(viewModel.Password),
                    IsAdmin = false
                };
                _userDbContext.users.Add(newUser);
                _userDbContext.SaveChanges();

                return RedirectToAction("Login", "Auth");

            }
            else
            {

                return View(viewModel);
            }
        }



        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginView)
        {
            if (_userManager.Login(loginView.Login, loginView.Password))
            {
                return RedirectToAction("Index", "Home");

            }

            ModelState.AddModelError("all", "Данные введены неверно");
            return View(loginView);
        }
    }
}
