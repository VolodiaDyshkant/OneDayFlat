using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OneDayFlat.Controllers
{
    public class UserLoggedInController : Controller
    {
        [Authorize]
        public IActionResult HomePage()
        {
            return View();
        }
    }
}