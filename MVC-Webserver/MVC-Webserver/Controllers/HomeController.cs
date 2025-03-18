using Dapper;
using Microsoft.AspNetCore.Mvc;
using MVC_Webserver.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MVC_Webserver.Controllers
{
    public class HomeController : Controller
    {
        //ILogger<HomeController> logs errors specific for HomeController
        private readonly ILogger<HomeController> _logger; 

        // Constructor to inject logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Index action returns an empty view
        public IActionResult Index()
        {
            return View(); // View - Front page
        }

        public IActionResult Privacy()
        {
            return View(); // Returns a HTML-page with Razor view
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
