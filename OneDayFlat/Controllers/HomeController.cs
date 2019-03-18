using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using OneDayFlat.Data;
using OneDayFlat.Models;

namespace OneDayFlat.Controllers
{
    public class HomeController : Controller
    {
        //[Authorize]
        OneDayFlatContext _context;

        public HomeController(OneDayFlatContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var currentUser = new User();
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Sid).Value);
                currentUser = _context.User.FirstOrDefault(user => user.UserID == userId);
            }

            return View(currentUser);
        }
        
        
       
    
    }
}