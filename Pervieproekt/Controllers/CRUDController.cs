using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pervieproekt.Models;

namespace Pervieproekt.Controllers
{
	public class CRUDController : Controller
	{
		public SushiStoreContext db;
		public CRUDController(SushiStoreContext context)
		{
			db = context;
		}
		public IActionResult Create()
		{
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> Create(Catalogg catalogg)
        {
            db.Cataloggs.Add(catalogg);
            await db.SaveChangesAsync();
            return RedirectToAction("vibor");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Catalogg catalogg = await db.Cataloggs.FirstOrDefaultAsync(p => p.IdCatalog == id);
                if (catalogg != null)
                    return View(catalogg);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Catalogg catalogg = await db.Cataloggs.FirstOrDefaultAsync(p => p.IdCatalog == id);
                if (catalogg != null)
                {
                    db.Cataloggs.Remove(catalogg);
                    await db.SaveChangesAsync();
                    return RedirectToAction("vibor");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id != null)
            {
                Catalogg catalogg = await db.Cataloggs.FirstOrDefaultAsync(p => p.IdCatalog == id);
                if (catalogg != null)
                    return View(catalogg);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(Catalogg catalogg)
        {
            db.Cataloggs.Update(catalogg);
            await db.SaveChangesAsync();
            return RedirectToAction("vibor");
        }
        public async Task<IActionResult> Read(int? id)
        {
            if (id != null)
            {
                Catalogg catalogg = await db.Cataloggs.FirstOrDefaultAsync(p => p.IdCatalog == id);
                if (catalogg != null)
                    return View(catalogg);
            }
            return NotFound();
        }
        public async Task<IActionResult>vibor()
        {
            return View(await db.Cataloggs.ToListAsync());
        }

	}
}
