using Microsoft.EntityFrameworkCore;
using ZooManagementSystem.Data.Repositories;
using ZooManagementSystem.Models.Entities;
using ZooManagementSystem.Models.ViewModels;
using ZooManagementSystem.Services.Interfaces;

namespace ZooManagementSystem.Services.Implementations
{
    public class AnimalQueryService : IAnimalQueryService
    {
        private readonly IGenericRepository<Animal> _animalRepository;

        public AnimalQueryService(IGenericRepository<Animal> animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<List<AnimalCardViewModel>> GetAnimalCardsAsync(string? especie = null, CancellationToken cancellationToken = default)
        {
            var query = _animalRepository.QueryNoTracking()
                .Include(a => a.Jaula)!
                    .ThenInclude(j => j!.Zona)!
                        .ThenInclude(z => z!.Ecosistema)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(especie))
            {
                query = query.Where(a => a.Especie != null && a.Especie.Trim() == especie.Trim());
            }

            return await query
                .OrderBy(a => a.Especie)
                .ThenBy(a => a.NombrePopular)
                .Select(a => new AnimalCardViewModel
                {
                    Id = a.Id,
                    Nombre = (a.NombrePopular ?? string.Empty).Trim(),
                    Especie = (a.Especie ?? string.Empty).Trim(),
                    Ecosistema = a.Jaula != null && a.Jaula.Zona != null && a.Jaula.Zona.Ecosistema != null
                        ? (a.Jaula.Zona.Ecosistema.Descripcion ?? string.Empty)
                        : string.Empty,
                    Dieta = CompatibilityService.InferDiet((a.Especie ?? string.Empty).Trim()).ToString(),
                    FotoUrl = a.UrlImagen,
                    Jaula = a.Jaula != null ? a.Jaula.Codigo.Trim() : string.Empty,
                    Zona = a.Jaula != null && a.Jaula.Zona != null ? a.Jaula.Zona.Descripcion : string.Empty
                })
                .ToListAsync(cancellationToken);
        }
    }
}
