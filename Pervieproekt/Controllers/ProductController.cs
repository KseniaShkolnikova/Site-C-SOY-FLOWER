using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pervieproekt.Models;
using System.Diagnostics;

namespace Pervieproekt.Controllers
{
    
    public class ProductController : Controller
    {

        public readonly SushiStoreContext db;

        public ProductController(SushiStoreContext context)
        {
            db = context;

        }
        public IActionResult Catalog(string search, string productType, string sort)
        {
            var productTypes = db.ProductTypes.Select(pt => pt.TypeName).ToList();
            var productsQuery = db.Cataloggs.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                productsQuery = productsQuery.Where(p => p.Namee.Contains(search));
            }
            if (!string.IsNullOrEmpty(productType))
            {
                productsQuery = productsQuery.Where(p => p.ProductType.TypeName == productType);
            }
            switch (sort)
            {
                case "price_asc":
                    productsQuery = productsQuery.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    productsQuery = productsQuery.OrderByDescending(p => p.Price);
                    break;
                default:
                    productsQuery = productsQuery.OrderBy(p => p.Namee); 
                    break;
            }
            var products = productsQuery.ToList();
            ViewBag.ProductTypes = productTypes; 
            return View(products);
        }
        public IActionResult Information()
        {
            return View();
        }
        public  IActionResult Cart()
        {
            var userId = User.Identity?.Name;
            if (userId == null)
            {
                return RedirectToAction("Regist", "Autorization");
            }
            var cartItem = db.PosOrders
                .Where(c => c.UserrId == userId)
                .Include(c => c.Catalog)
                .ToList();


            return View(cartItem);
        }
        public IActionResult Payment()
        {
            return View();
        }
        public async Task<IActionResult> AddCount(int productId, int quantiti)
        {
            var userid = User.Identity?.Name;
            var product = await db.PosOrders.FindAsync(productId);
            var cartItem = await db.PosOrders.FirstOrDefaultAsync(c => c.IdPosOrders == productId && c.UserrId == userid);
            cartItem.Countt = quantiti;
            cartItem.Price = (decimal)(cartItem.Countt * product.Price);
            await db.SaveChangesAsync();
            return RedirectToAction("Cart", "Product");

        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? productId)
        {
            if (productId != null)
            {
                PosOrder catalogg = await db.PosOrders.FirstOrDefaultAsync(p => p.IdPosOrders == productId);
                if (catalogg != null)
                    return View(catalogg);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? productId)
        {
            if (productId != null)
            {
                PosOrder catalogg = await db.PosOrders.FirstOrDefaultAsync(p => p.IdPosOrders == productId);
                if (catalogg != null)
                {
                    db.PosOrders.Remove(catalogg);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Cart", "Product");
                }
            }
            return NotFound();
        }




        public async Task<IActionResult> AddtoCart(int productid, int quantiti)
        {
            var userid = User.Identity?.Name;
            if (userid == null)
            {
                return RedirectToAction("Regist", "Autorization");
            }

            var product = await db.Cataloggs.FindAsync(productid);
            if (product == null)
            {
                return NotFound("Продукт не найден");
            }

            var cartItem = await db.PosOrders.FirstOrDefaultAsync(c => c.CatalogId == productid && c.UserrId == userid);

            if (cartItem == null)
            {
                cartItem = new PosOrder
                {
                    UserrId = userid,
                    CatalogId = productid,
                    Price = product.Price * quantiti,
                    Countt = quantiti
                };
                db.PosOrders.Add(cartItem);
            }
            else
            {
                cartItem.Countt += quantiti;
                cartItem.Price = cartItem.Countt * product.Price;
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Cart", "Product");
        }

    }
}
