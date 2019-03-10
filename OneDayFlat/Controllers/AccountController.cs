using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneDayFlat.Data;
using OneDayFlat.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace OneDayFlat.Controllers
{
    public class AccountController : Controller
    {
       
        private OneDayFlatContext db;
        public AccountController(OneDayFlatContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(ViewModels.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.User
                     .Include(u => u.Role)
                     .FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); 

                    return RedirectToAction("HomePage", "UserLoggedIn");
                }
                ModelState.AddModelError("", "Некоректні логін і(або) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ViewModels.RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.User.FirstOrDefaultAsync(u => u.Login == model.Login);
                if (user == null)
                {
                    
                    user = new User {Name=model.Name, Login = model.Login, Number=model.Number, Password = model.Password };
                    Role userRole = await db.Role.FirstOrDefaultAsync(r => r.Name == "user");
                    if (userRole != null)
                    {
                        user.Role = userRole;
                    }
                    db.User.Add(user);
                    await db.SaveChangesAsync();

                    await Authenticate(user); 

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некоректні логін і(або) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            
            var claims = new List<Claim>
            {
                //new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
           
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}