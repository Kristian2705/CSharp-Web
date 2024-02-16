using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskBoard.Models;

namespace TaskBoard.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}