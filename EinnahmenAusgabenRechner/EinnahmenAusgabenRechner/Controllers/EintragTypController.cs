
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EinnahmenAusgabenRechner.Models;

public class EintragTypController : Controller
{
    private readonly EinnahmenAusgabenRechnerContext _context;

    public EintragTypController(EinnahmenAusgabenRechnerContext context)
    {
        _context = context;
    }

    // GET: EintragTYPS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.EintragTyp.ToListAsync());
    }

    // GET: EintragTYPS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var Eintragtyp = await _context.EintragTyp
            .FirstOrDefaultAsync(m => m.Id == id);
        if (Eintragtyp == null)
        {
            return NotFound();
        }

        return View(Eintragtyp);
    }

    // GET: EintragTYPS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: EintragTYPS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, se http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Bezeichnung,Eintrag")] EintragTyp Eintragtyp)
    {
        if (ModelState.IsValid)
        {
            _context.Add(Eintragtyp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(Eintragtyp);
    }

    // GET: EintragTYPS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var Eintragtyp = await _context.EintragTyp.FindAsync(id);
        if (Eintragtyp == null)
        {
            return NotFound();
        }
        return View(Eintragtyp);
    }

    // POST: EintragTYPS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, se http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Bezeichnung,Eintrag")] EintragTyp Eintragtyp)
    {
        if (id != Eintragtyp.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(Eintragtyp);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EintragTypExists(Eintragtyp.Id))
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
        return View(Eintragtyp);
    }

    // GET: EintragTYPS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var Eintragtyp = await _context.EintragTyp
            .FirstOrDefaultAsync(m => m.Id == id);
        if (Eintragtyp == null)
        {
            return NotFound();
        }

        return View(Eintragtyp);
    }

    // POST: EintragTYPS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var Eintragtyp = await _context.EintragTyp.FindAsync(id);
        if (Eintragtyp != null)
        {
            _context.EintragTyp.Remove(Eintragtyp);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EintragTypExists(int? id)
    {
        return _context.EintragTyp.Any(e => e.Id == id);
    }
}
