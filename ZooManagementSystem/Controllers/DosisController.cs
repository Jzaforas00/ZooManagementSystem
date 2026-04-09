using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagementSystem.Data;
using ZooManagementSystem.Data.Repositories;
using ZooManagementSystem.Models.Entities;

namespace ZooMvc.Controllers;

public class DosisController : Controller
{
    private readonly IGenericRepository<Dosis> _repository;
    private readonly ZooDbContext _context;

    public DosisController(IGenericRepository<Dosis> repository, ZooDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var items = await _repository.QueryNoTracking()
            .Include(d => d.Animal)
            .Include(d => d.Alimento)
            .OrderBy(d => d.DosisId)
            .ToListAsync(cancellationToken);
        return View(items);
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var item = await _repository.QueryNoTracking()
            .Include(d => d.Animal)
            .Include(d => d.Alimento)
            .FirstOrDefaultAsync(d => d.DosisId == id, cancellationToken);
        if (item is null) return NotFound();
        return View(item);
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        await LoadCombosAsync(cancellationToken);
        return View(new Dosis());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Dosis item, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await LoadCombosAsync(cancellationToken);
            return View(item);
        }

        await _repository.AddAsync(item, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(id, cancellationToken);
        if (item is null) return NotFound();

        await LoadCombosAsync(cancellationToken);
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Dosis item, CancellationToken cancellationToken)
    {
        if (id != item.DosisId) return BadRequest();

        if (!ModelState.IsValid)
        {
            await LoadCombosAsync(cancellationToken);
            return View(item);
        }

        _repository.Update(item);
        await _repository.SaveChangesAsync(cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var item = await _repository.QueryNoTracking()
            .Include(d => d.Animal)
            .Include(d => d.Alimento)
            .FirstOrDefaultAsync(d => d.DosisId == id, cancellationToken);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(id, cancellationToken);
        if (item is not null)
        {
            _repository.Remove(item);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadCombosAsync(CancellationToken cancellationToken)
    {
        ViewBag.Animales = await _context.Animales.AsNoTracking().OrderBy(a => a.NombrePopular).ToListAsync(cancellationToken);
        ViewBag.Alimentos = await _context.Alimentos.AsNoTracking().OrderBy(a => a.Nombre).ToListAsync(cancellationToken);
    }
}
