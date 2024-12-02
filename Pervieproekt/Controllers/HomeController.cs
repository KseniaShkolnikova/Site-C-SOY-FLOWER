using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pervieproekt.Models;
using System.Diagnostics;

namespace Pervieproekt.Controllers
{
    public class HomeController : Controller
    {
        public SushiStoreContext db;
        public HomeController(SushiStoreContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}