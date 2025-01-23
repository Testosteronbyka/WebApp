using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppSuperheroes.Context;
using AppSuperheroes.Entities;
using System.Threading.Tasks;

namespace AppSuperheroes.Controllers
{
    public class SuperheroController : Controller
    {
        private readonly MyDbContext _context;

        // Konstruktor przyjmujący MyDbContext
        public SuperheroController(MyDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index(int page = 1, int size = 20)
        {
            // Liczba rekordów w tabeli Superheroes
            var totalRecords = await _context.Superheroes.CountAsync();

            // Pobieranie danych z relacjami z uwzględnieniem stronicowania
            var superheroes = await _context.Superheroes
                .Include(s => s.Alignment)         // Łączymy z Alignment
                .Include(s => s.Gender)            // Łączymy z Gender
                .Include(s => s.HeroPowers)        // Łączymy z HeroPowers
                .ThenInclude(hp => hp.Power)   // Łączymy z Superpower
                .OrderBy(s => s.Id)                // Sortujemy po Id superbohatera
                .Skip((page - 1) * size)           // Pomijamy wcześniejsze strony
                .Take(size)                        // Pobieramy tylko bieżącą stronę
                .AsNoTracking()                    // Nie śledzimy obiektów w kontekście
                .ToListAsync();

            // Przekazujemy dane paginacji do ViewBag
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalRecords / (double)size);
            ViewBag.PageSize = size;

            // Zwracamy dane do widoku
            return View(superheroes);
        }
        
        
        
        
        [HttpGet]
        public IActionResult Create()
        {
            // Ładowanie list Gender, Alignment i Superpowers do ViewBag
            ViewBag.GenderList = _context.Genders.ToList();
            ViewBag.AlignmentList = _context.Alignments.ToList();
            ViewBag.Superpowers = _context.Superpowers.ToList(); // Dodaj Superpowers
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Superhero superhero, List<int> superpowerIds)
        {
            if (ModelState.IsValid)
            {
                _context.Add(superhero);

                // Dodawanie supermocy
                foreach (var powerId in superpowerIds)
                {
                    var heroPower = new HeroPower
                    {
                        HeroId = superhero.Id,
                        PowerId = powerId
                    };
                    _context.HeroPowers.Add(heroPower);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  // Po zapisaniu przekierowujemy na stronę główną
            }
            // Jeśli dane są niepoprawne, zwracamy formularz
            ViewBag.GenderList = _context.Genders.ToList();
            ViewBag.AlignmentList = _context.Alignments.ToList();
            ViewBag.Superpowers = _context.Superpowers.ToList(); // Dodaj Superpowers
            return View(superhero);
        }

        // Akcja GET do edytowania superbohatera
        public IActionResult Edit(int id)
        {
            var superhero = _context.Superheroes
                .Include(h => h.HeroPowers)
                    .ThenInclude(hp => hp.Power)
                .FirstOrDefault(h => h.Id == id);

            if (superhero == null)
            {
                return NotFound();
            }

            // Ładujemy dane do ViewBag
            ViewBag.Superpowers = _context.Superpowers.ToList();
            return View(superhero);
        }

        // Akcja POST do edytowania superbohatera
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Superhero superhero, List<int> superpowerIds)
        {
            if (id != superhero.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(superhero);

                    // Usuń istniejące przypisania mocy
                    var currentHeroPowers = _context.HeroPowers.Where(hp => hp.HeroId == id).ToList();
                    _context.HeroPowers.RemoveRange(currentHeroPowers);

                    // Dodaj nowe przypisania mocy
                    foreach (var powerId in superpowerIds)
                    {
                        var heroPower = new HeroPower
                        {
                            HeroId = superhero.Id,
                            PowerId = powerId
                        };
                        _context.HeroPowers.Add(heroPower);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Superheroes.Any(h => h.Id == superhero.Id))
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

            // Jeśli dane są niepoprawne, zwracamy formularz
            ViewBag.Superpowers = _context.Superpowers.ToList();
            return View(superhero);
        }

        // Akcja GET do usuwania superbohatera
        public IActionResult Delete(int id)
        {
            var superhero = _context.Superheroes
                .Include(h => h.HeroPowers)
                    .ThenInclude(hp => hp.Power)
                .FirstOrDefault(h => h.Id == id);

            if (superhero == null)
            {
                return NotFound();
            }

            return View(superhero);
        }

        // Akcja POST do usuwania superbohatera
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var superhero = await _context.Superheroes.FindAsync(id);
            if (superhero != null)
            {
                var heroPowers = _context.HeroPowers.Where(hp => hp.HeroId == id).ToList();
                _context.HeroPowers.RemoveRange(heroPowers);
                _context.Superheroes.Remove(superhero);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
