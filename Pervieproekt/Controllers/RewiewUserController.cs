using Microsoft.AspNetCore.Mvc;
using Pervieproekt.Models;

namespace Pervieproekt.Controllers
{
    public class RewiewUserController : Controller
    {
        public SushiStoreContext db;
        public RewiewUserController(SushiStoreContext context)
        {
            db = context;
        }
        public IActionResult Rewiews()
        {
            return View();
        }
    }
}
