using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppSuperheroes.Context;
using AppSuperheroes.Entities;
using System.Threading.Tasks;

namespace AppSuperheroes.Controllers
{
    public class SuperpowerController : Controller
    {
        private readonly MyDbContext _context;

        public SuperpowerController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Superpower/Index
        public async Task<IActionResult> Index()
        {
            // Pobieramy wszystkie supermoce z bazy danych (tylko właściwości mapowane do bazy)
            var superpowers = await _context.Superpowers.ToListAsync();
            return View(superpowers);
        }

        // GET: Superpower/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Superpower superpower)
        {
            // Znajdź największe istniejące Id w tabeli Superpowers
            int maxId = _context.Superpowers.Any() 
                ? _context.Superpowers.Max(sp => sp.Id) 
                : 0;

            // Przypisz nowe Id
            superpower.Id = maxId + 1;

            if (ModelState.IsValid)
            {
                _context.Add(superpower);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(superpower);
        }
    }
}