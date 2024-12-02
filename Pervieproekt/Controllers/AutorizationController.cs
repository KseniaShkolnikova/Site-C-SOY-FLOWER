using Microsoft.AspNetCore.Mvc;
using Pervieproekt.Models;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Pervieproekt.Controllers
{
    public class AutorizationController : Controller
    {

        private readonly SushiStoreContext _context;
        public AutorizationController(SushiStoreContext context)
        {
            _context = context;
        }
        private string HashPassword(string password)
        {
            using(var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-","").ToLower();
            }
        }
        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Enter(string password,string login)
        {
            string hashedpass = HashPassword(password);

            var user = _context.Users.FirstOrDefault(u=>u.Loginn == login && u.Passwordd == hashedpass);
            if (user != null)
            {
                var claim = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Mail),
                    new Claim(ClaimTypes.Name, user.Namee)
                    //new Claim(ClaimTypes.Role,user.roles == 1? "Customer":"OtherRole")
                };

                var claimIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return RedirectToAction("Catalog","Product");
            }
            ViewBag.ErrorMessage = "Не удалось войти";
            return View();
        }

        [HttpGet]
        public IActionResult Regist()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Regist(string name, string login, string password, string mail)
        {
            if(_context.Users.Any(u => u.Mail == mail))
            {
                ViewBag.ErrorMessage = "Не удалось зарегестрироваться";
                return View();
            }
            string hashpassword = HashPassword(password);
            var user = new User
            {
                Namee = name,
                Loginn = login,
                Passwordd = hashpassword,
                Mail = mail
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Enter", "Autorization");
        }
    }
}
