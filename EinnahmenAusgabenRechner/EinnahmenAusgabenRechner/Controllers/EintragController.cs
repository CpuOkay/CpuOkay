
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EinnahmenAusgabenRechner.Models;
using EinnahmenAusgabenRechner.ViewModels;

public class EintragController : Controller
{
    private readonly EinnahmenAusgabenRechnerContext _context;

    public EintragController(EinnahmenAusgabenRechnerContext context)
    {
        _context = context;
    }

    // GET: EintragS
    public async Task<IActionResult> Index(EintragenViewModel viewmodel)    
    {
        SetViewData();
        var query = _context.Eintrag.Include(b => b.Kategorie)
            .Include(b => b.EintragTyp).AsQueryable();

        if (viewmodel.SearchDatumVon.HasValue)
        {
            query = query.Where(b => b.Datum >= viewmodel.SearchDatumVon.Value);
        }

        if (viewmodel.SearchDatumBis.HasValue)
        {
            query = query.Where(b => b.Datum <= viewmodel.SearchDatumBis.Value);
        }

        if (!string.IsNullOrWhiteSpace(viewmodel.SearchBeschreibung))
        {
            query = query.Where(b => b.Beschreibung == viewmodel.SearchBeschreibung);
           
        }
        if(viewmodel.SearchKategorie.HasValue)
        {
            query = query.Where(b => b.KategorieId == viewmodel.SearchKategorie);
        }
        if (viewmodel.SearchEintragTyp.HasValue)
        {
            query = query.Where(b => b.EintragTypId == viewmodel.SearchEintragTyp);
        }
        if(viewmodel.AnzahlEintraege.HasValue)
        {
            query = query.Take(viewmodel.AnzahlEintraege.Value);
        }

        viewmodel.Eintragen = await query.ToListAsync();
        return View(viewmodel);
    }

    // GET: EintragS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var Eintrag = await _context.Eintrag
            .FirstOrDefaultAsync(m => m.Id == id);
        if (Eintrag == null)
        {
            return NotFound();
        }

        return View(Eintrag);
    }

    // GET: EintragS/Create
    public IActionResult Create()
    {
        SetViewData();
        return View();
    }

    // POST: EintragS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, se http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Betrag,Datum,Beschreibung,KategorieId,EintragTypId,Kategorie,EintragTyp")] Eintrag Eintrag)
    {
        SetViewData();
        if (ModelState.IsValid)
        {
            _context.Add(Eintrag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(Eintrag);
    }

    // GET: EintragS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        SetViewData();
        if (id == null)
        {
            return NotFound();
        }

        var Eintrag = await _context.Eintrag.FindAsync(id);
        if (Eintrag == null)
        {
            return NotFound();
        }
        return View(Eintrag);
    }

    // POST: EintragS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, se http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Betrag,Datum,Beschreibung,KategorieId,EintragTypId,Kategorie,EintragTyp")] Eintrag Eintrag)
    {
        SetViewData();
        if (id != Eintrag.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(Eintrag);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EintragExists(Eintrag.Id))
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
        return View(Eintrag);
    }

    // GET: EintragS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var Eintrag = await _context.Eintrag
            .FirstOrDefaultAsync(m => m.Id == id);
        if (Eintrag == null)
        {
            return NotFound();
        }

        return View(Eintrag);
    }

    // POST: EintragS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var Eintrag = await _context.Eintrag.FindAsync(id);
        if (Eintrag != null)
        {
            _context.Eintrag.Remove(Eintrag);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EintragExists(int? id)
    {
        return _context.Eintrag.Any(e => e.Id == id);
    }
    private void SetViewData()
    {
        var Eintragstypen = _context.EintragTyp.ToList();
        var kategorien = _context.Kategorie.ToList();
        ViewData["Eintragstypen"] = Eintragstypen;
        ViewData["kategorien"] =    kategorien;
    }

}
