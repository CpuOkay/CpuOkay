
using EinnahmenAusgabenRechner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;

public class KategorieController : Controller
{
    private readonly EinnahmenAusgabenRechnerContext _context;

    public KategorieController(EinnahmenAusgabenRechnerContext context)
    {
        _context = context;
    }

    // GET: KATEGORIES
    public async Task<IActionResult> Index()
    {
        return View(await _context.Kategorie.ToListAsync());
    }

    // GET: KATEGORIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var kategorie = await _context.Kategorie
            .FirstOrDefaultAsync(m => m.Id == id);
        if (kategorie == null)
        {
            return NotFound();
        }

        return View(kategorie);
    }

    // GET: KATEGORIES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: KATEGORIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, se http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Bezeichnung,Farbe,Eintrag")] Kategorie kategorie)
    {
        if (ModelState.IsValid)
        {
            _context.Add(kategorie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(kategorie);
    }

    // GET: KATEGORIES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var kategorie = await _context.Kategorie.FindAsync(id);
        if (kategorie == null)
        {
            return NotFound();
        }
        return View(kategorie);
    }

    // POST: KATEGORIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, se http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Bezeichnung,Farbe,Eintrag")] Kategorie kategorie)
    {
        if (id != kategorie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(kategorie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Kategoriexists(kategorie.Id))
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
        return View(kategorie);
    }

    // GET: KATEGORIES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var kategorie = await _context.Kategorie
            .FirstOrDefaultAsync(m => m.Id == id);
        if (kategorie == null)
        {
            return NotFound();
        }

        return View(kategorie);
    }

    // POST: KATEGORIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {

        var kategorie = await _context.Kategorie
            .Include(k => k.Eintrag)
            .FirstOrDefaultAsync(k => k.Id == id);

        if (kategorie == null)
        {
            return NotFound();
        }

        const string ohneKat = "Ohne Kategorie";

        if (kategorie.Bezeichnung == ohneKat)
        {
            return RedirectToAction(nameof(Index));
        }

        if (kategorie.Eintrag.Any())
        {
            var archivKategorie = await _context.Kategorie
                .FirstOrDefaultAsync(k => k.Bezeichnung == ohneKat);

            if (archivKategorie == null)
            {
                archivKategorie = new Kategorie
                {
                    Bezeichnung = ohneKat
                };

                _context.Kategorie.Add(archivKategorie);

                await _context.SaveChangesAsync();
            }

            foreach (var eintrag in kategorie.Eintrag)
            {
                eintrag.KategorieId = archivKategorie.Id;
            }
        }

        _context.Kategorie.Remove(kategorie);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));

    }

    private bool Kategoriexists(int? id)
    {
        return _context.Kategorie.Any(e => e.Id == id);
    }
}
