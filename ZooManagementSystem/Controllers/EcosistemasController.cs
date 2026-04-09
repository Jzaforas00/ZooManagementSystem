using Microsoft.AspNetCore.Mvc;
using ZooManagementSystem.Data.Repositories;
using ZooManagementSystem.Models.Entities;

namespace ZooMvc.Controllers;

public class EcosistemasController : Controller
{
    private readonly IGenericRepository<Ecosistema> _repository;

    public EcosistemasController(IGenericRepository<Ecosistema> repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return View(items.OrderBy(x => x.Descripcion));
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(id, cancellationToken);
        if (item is null) return NotFound();
        return View(item);
    }

    public IActionResult Create() => View(new Ecosistema());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Ecosistema item, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View(item);

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
    public async Task<IActionResult> Edit(int id, Ecosistema item, CancellationToken cancellationToken)
    {
        if (id != item.Id) return BadRequest();
        if (!ModelState.IsValid) return View(item);

        _repository.Update(item);
        await _repository.SaveChangesAsync(cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(id, cancellationToken);
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
