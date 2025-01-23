using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppSuperheroes.Context;
using AppSuperheroes.Entities;

namespace AppSuperheroes.Controllers
{
    public class HeroAttributeController : Controller
    {
        private readonly MyDbContext _context;

        public HeroAttributeController(MyDbContext context)
        {
            _context = context;
        }

        // GET: HeroAttribute
        public async Task<IActionResult> Index()
        {
            var heroAttributes = await _context.HeroAttributes.ToListAsync();
            return View(heroAttributes);
        }

        private IActionResult View(object heroAttributes)
        {
            throw new NotImplementedException();
        }

        // GET: HeroAttribute/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var heroAttribute = await _context.HeroAttributes
                .FirstOrDefaultAsync(m => m.HeroId == id);
            if (heroAttribute == null)
            {
                return NotFound();
            }

            return View(heroAttribute);
        }

        // GET: HeroAttribute/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HeroAttribute/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HeroId,AttributeId,AttributeValue")] HeroAttribute heroAttribute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(heroAttribute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(heroAttribute);
        }

        // GET: HeroAttribute/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var heroAttribute = await _context.HeroAttributes.FindAsync(id);
            if (heroAttribute == null)
            {
                return NotFound();
            }
            return View(heroAttribute);
        }

        // POST: HeroAttribute/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HeroId,AttributeId,AttributeValue")] HeroAttribute heroAttribute)
        {
            if (id != heroAttribute.HeroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(heroAttribute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeroAttributeExists(heroAttribute.HeroId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(heroAttribute);
        }

        // GET: HeroAttribute/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var heroAttribute = await _context.HeroAttributes
                .FirstOrDefaultAsync(m => m.HeroId == id);
            if (heroAttribute == null)
            {
                return NotFound();
            }

            return View(heroAttribute);
        }

        // POST: HeroAttribute/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var heroAttribute = await _context.HeroAttributes.FindAsync(id);
            _context.HeroAttributes.Remove(heroAttribute);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeroAttributeExists(int id)
        {
            return _context.HeroAttributes.Any(e => e.HeroId == id);
        }
    }
}
