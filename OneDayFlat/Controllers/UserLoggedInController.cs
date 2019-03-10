using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneDayFlat.Models;

namespace OneDayFlat.Controllers
{
    public class UserLoggedInController : Controller
    {
        [Authorize(Roles="admin")]
        public IActionResult HomePage()
        {
            return View();
        }
    }
}