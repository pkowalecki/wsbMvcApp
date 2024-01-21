using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Security.Claims;
using wsbMvcApp.Models;
using Microsoft.EntityFrameworkCore;
using wsbMvcApp.Data;

namespace wsbMvcApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly wsbMvcAppContext _context;
        private List<User> _users = new List<User>
    {
        new User { Id = 1, Username = "test", Password = "test" }, // Tymczasowy użytkownik
    };

        public AccountController(wsbMvcAppContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View(new User());
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(User user)
        {
            var validUser = _users.SingleOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (validUser == null)
            {
                // Sprawdzenie w bazie danych
                validUser = _context.User.SingleOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            }

            if (validUser != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                HttpContext.Session.SetString("UserId", validUser.Id.ToString());
                HttpContext.Session.SetString("Username", validUser.Username);

                return RedirectToAction("Index", "Meals");
            }
            else
            {
                ViewBag.Message = "Bad data";
                return View("Login", user);
            }
        }
    }
}
