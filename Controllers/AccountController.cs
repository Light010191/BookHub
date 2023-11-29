using BooksHub.Models.Identity;
using BooksHub.Models;
using BooksHub.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BooksHub.Services;

namespace BooksHub.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserDbContext _userDbContext;
        
        public SignInManager<AppUser> SignInManager { get; set; }
        public UserManager<AppUser> UserManager { get; private set; }

        private readonly MailSenderService mailSenderService;
        public AccountController(UserDbContext _userDbContext, UserManager<AppUser> userManager, SignInManager<AppUser> singManager,
            MailSenderService mailSenderService)

        {
            this._userDbContext = _userDbContext;
            this.UserManager = userManager;
            this.SignInManager = singManager;
            this.mailSenderService = mailSenderService;

        }
        public IActionResult Register()
        {
            return View();
        }





        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    FullName = registerViewModel.FullName,
                    UserName = registerViewModel.Email,
                    Age = registerViewModel.Year
                };
                var result = await UserManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                   
                    var URL = Url.Action("ConfirmEmail",
                                        "Account",
                                        new { userId = user.Id, token = token },
                                        protocol: HttpContext.Request.Scheme);

                    mailSenderService.Send(registerViewModel.Email, "Please confirm your registration", URL);
                    // await signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(err.Code, $"{err.Description}");

                    }
                }
            }
            return View();
        }

        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            var user = await UserManager.FindByNameAsync(loginViewModel.Login);
            if (user != null)
            {
                if (await UserManager.IsEmailConfirmedAsync(user))
                {
                    if (await UserManager.CheckPasswordAsync(user, loginViewModel.Password))
                    {
                        await SignInManager.SignInAsync(user, loginViewModel.RememberMe);
                        if (!string.IsNullOrWhiteSpace(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Login", "Incorrect Username or Password");
                    }

                }
                else
                {
                    ModelState.AddModelError("Login", "Please confirm your email");
                }


            }
            else
            {
                ModelState.AddModelError("Login", "Incorrect Username or Password");
            }


            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await UserManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    ViewBag.Message = "Email confirmed";
                }
                else
                {
                    ViewBag.Message = "Error";
                }
            }
            else
            {
                ViewBag.Message = "Error";
            }

            return View();

        }
    }
}
