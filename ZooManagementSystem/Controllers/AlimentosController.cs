using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagementSystem.Data;
using ZooManagementSystem.Data.Repositories;
using ZooManagementSystem.Models.Entities;

namespace ZooMvc.Controllers;

public class AlimentosController : Controller
{
    private readonly IGenericRepository<Alimento> _repository;
    private readonly ZooDbContext _context;

    public AlimentosController(IGenericRepository<Alimento> repository, ZooDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var items = await _repository.QueryNoTracking()
            .Include(a => a.Proveedor)
            .OrderBy(a => a.Nombre)
            .ToListAsync(cancellationToken);
        return View(items);
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var item = await _repository.QueryNoTracking()
            .Include(a => a.Proveedor)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        if (item is null) return NotFound();
        return View(item);
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        await LoadCombosAsync(cancellationToken);
        return View(new Alimento());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Alimento item, CancellationToken cancellationToken)
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
    public async Task<IActionResult> Edit(int id, Alimento item, CancellationToken cancellationToken)
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
            .Include(a => a.Proveedor)
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

    private async Task LoadCombosAsync(CancellationToken cancellationToken)
    {
        ViewBag.Proveedores = await _context.Proveedores.AsNoTracking().OrderBy(p => p.Nombre).ToListAsync(cancellationToken);
    }
}
