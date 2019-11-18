using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nata.Models;
using nata.Data;

namespace nata.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NataDbContext _nataDbContext;

        public HomeController(ILogger<HomeController> logger, NataDbContext nataDbContext)
        {
            _logger = logger;
            _nataDbContext = nataDbContext;
        }

        public IActionResult Index()
        {
            var paises = _nataDbContext.Countries.ToList();
            return View();
        }

        public IActionResult About()
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
