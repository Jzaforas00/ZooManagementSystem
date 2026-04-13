using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagementSystem.Data;
using ZooManagementSystem.Data.Repositories;
using ZooManagementSystem.Models.Entities;

namespace ZooManagementSystem.Controllers;

public class AlimentacionController : Controller
{
    private readonly IGenericRepository<Alimentacion> _repository;
    private readonly ZooDbContext _context;

    public AlimentacionController(IGenericRepository<Alimentacion> repository, ZooDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var items = await _repository.QueryNoTracking()
            .Include(a => a.Animales)
            .OrderBy(a => a.Nombre)
            .ToListAsync(cancellationToken);

        return View(items);
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var item = await _repository.QueryNoTracking()
            .Include(a => a.Animales)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (item is null) return NotFound();

        return View(item);
    }

    public IActionResult Create()
    {
        return View(new Alimentacion());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Alimentacion item, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
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

        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Alimentacion item, CancellationToken cancellationToken)
    {
        if (id != item.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            return View(item);
        }

        _repository.Update(item);
        await _repository.SaveChangesAsync(cancellationToken);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var item = await _repository.QueryNoTracking()
            .Include(a => a.Animales)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

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
}