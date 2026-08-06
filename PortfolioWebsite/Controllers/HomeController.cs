using Microsoft.AspNetCore.Mvc;
using PortfolioWebsite.Models;
using System.Diagnostics;

namespace PortfolioWebsite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Skills()
        {
            return View();
        }
        public IActionResult Projects()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}
