using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nata.Data;
using nata.Models;

namespace nata.Controllers
{
    public class HomeController : Controller
    {
        private readonly NataDbContext _context;
        private readonly ApplicationDbContext _userContext;

        public HomeController(NataDbContext context, ApplicationDbContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public IActionResult Index()
        {

            return View();

        }

        public IActionResult About()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
