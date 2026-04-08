using Microsoft.EntityFrameworkCore;
using ZooManagementSystem.Data.Repositories;
using ZooManagementSystem.Models.Entities;
using ZooManagementSystem.Models.ViewModels;
using ZooManagementSystem.Services.Interfaces;

namespace ZooManagementSystem.Services.Implementations
{
    public class ManagementService : IManagementService
    {
        private readonly IGenericRepository<Dosis> _dosisRepository;
        private readonly IGenericRepository<Alimento> _alimentoRepository;

        public ManagementService(IGenericRepository<Dosis> dosisRepository, IGenericRepository<Alimento> alimentoRepository)
        {
            _dosisRepository = dosisRepository;
            _alimentoRepository = alimentoRepository;
        }

        public async Task<List<AnimalCostViewModel>> GetAnimalCostsAsync(CancellationToken cancellationToken = default)
        {
            return await _dosisRepository.QueryNoTracking()
                .Include(d => d.Alimento)
                .Include(d => d.Animal)
                .Where(d => d.Animal != null)
                .GroupBy(d => new { d.AnimalId, Nombre = d.Animal!.NombrePopular, Especie = d.Animal.Especie })
                .Select(g => new AnimalCostViewModel
                {
                    AnimalId = g.Key.AnimalId ?? 0,
                    Nombre = (g.Key.Nombre ?? string.Empty).Trim(),
                    Especie = (g.Key.Especie ?? string.Empty).Trim(),
                    Coste = g.Sum(x => (x.Cantidad ?? 0) * (x.Alimento != null ? (x.Alimento.Precio ?? 0m) : 0m))
                })
                .OrderByDescending(x => x.Coste)
                .ToListAsync(cancellationToken);
        }

        public async Task<decimal> GetFoodInvestmentAsync(CancellationToken cancellationToken = default)
        {
            return await _alimentoRepository.QueryNoTracking()
                .SumAsync(a => (a.Precio ?? 0m) * (a.Stock ?? 0), cancellationToken);
        }
    }
}
