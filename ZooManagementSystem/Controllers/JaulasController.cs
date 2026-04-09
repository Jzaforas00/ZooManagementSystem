using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagementSystem.Data;
using ZooManagementSystem.Data.Repositories;
using ZooManagementSystem.Models.Entities;

namespace ZooMvc.Controllers;

public class JaulasController : Controller
{
    private readonly IGenericRepository<Jaula> _repository;
    private readonly ZooDbContext _context;

    public JaulasController(IGenericRepository<Jaula> repository, ZooDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var items = await _repository.QueryNoTracking()
            .Include(j => j.Zona)
            .OrderBy(j => j.Codigo)
            .ToListAsync(cancellationToken);
        return View(items);
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var item = await _repository.QueryNoTracking()
            .Include(j => j.Zona)
                .ThenInclude(z => z!.Ecosistema)
            .Include(j => j.Animales)
            .Include(j => j.Empleados)
            .FirstOrDefaultAsync(j => j.Id == id, cancellationToken);
        if (item is null) return NotFound();
        return View(item);
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        await LoadCombosAsync(cancellationToken);
        return View(new Jaula());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Jaula item, CancellationToken cancellationToken)
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
    public async Task<IActionResult> Edit(int id, Jaula item, CancellationToken cancellationToken)
    {
        if (id != item.Id) return BadRequest();

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
            .Include(j => j.Zona)
            .FirstOrDefaultAsync(j => j.Id == id, cancellationToken);
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
        ViewBag.Zonas = await _context.Zonas.AsNoTracking().OrderBy(z => z.Descripcion).ToListAsync(cancellationToken);
    }
}
