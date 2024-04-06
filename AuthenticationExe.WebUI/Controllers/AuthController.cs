using AuthenticationExe.Business.Dtos;
using AuthenticationExe.Business.Service;
using AuthenticationExe.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthenticationExe.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterFormViewModel formData) 
        {
           if(!ModelState.IsValid)
            {
                ViewBag.ValidMessage = "Lütfen aşağıdaki hataları düzeltin :";
                return View(formData);
            }
            var registerDto = new RegisterDto()
            {
                Email = formData.Email.Trim(),
                Password = formData.Password,
                FirstName = formData.FirstName.Trim(),
                LastName = formData.LastName.Trim()
            };
            var result = _userService.AddUser(registerDto);
            if (!result.IsSucceed)
            {
                ViewBag.ValidMessage = result.Message;
                return View(formData);
            }
            TempData["RegisterMessage"] = result.Message;
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            var loginDto = new LoginDto()
            {
                Email = formData.Email,
                Password = formData.Password
            };
            var userInfo = _userService.LoginUser(loginDto);
            if(userInfo is null)
            {
                ViewBag.ErrorMessage = "Email yada Şifre bilgileri hatalı.";
                return View(formData);
            }

            var claims = new List<Claim>();

            claims.Add(new Claim("id", userInfo.Id.ToString()));
            claims.Add(new Claim("email", userInfo.Email));
            claims.Add(new Claim("firstName", userInfo.FirstName));
            claims.Add(new Claim("lastName", userInfo.LastName));
            claims.Add(new Claim("fullName", userInfo.FullName));

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var autProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(24))
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), autProperties);

            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
