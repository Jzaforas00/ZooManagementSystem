using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using ZooManagementSystem.Data.Repositories;
using ZooManagementSystem.Models.Entities;
using ZooManagementSystem.Models.Enums;
using ZooManagementSystem.Models.ViewModels;
using ZooManagementSystem.Services.Interfaces;

namespace ZooManagementSystem.Services.Implementations
{
    public class CompatibilityService(IGenericRepository<Jaula> jaulaRepository) : ICompatibilityService
    {
        public async Task<List<CageCompatibilityViewModel>> GetCageCompatibilityAsync(CancellationToken cancellationToken = default)
        {
            var jaulas = await jaulaRepository.QueryNoTracking()
                .Include(j => j.Animales)
                .OrderBy(j => j.Codigo)
                .ToListAsync(cancellationToken);

            return [.. jaulas.Select(j =>
        {
            var animales = j.Animales
                .Select(a => new AnimalInCageViewModel
                {
                    AnimalId = a.Id,
                    Nombre = (a.NombrePopular ?? string.Empty).Trim(),
                    Especie = (a.Especie ?? string.Empty).Trim(),
                    Dieta = InferDiet((a.Especie ?? string.Empty).Trim()).ToString()
                })
                .ToList();

            var dietas = animales.Select(a => ParseDiet(a.Dieta)).Distinct().ToList();
            var compatible = IsCompatible(dietas);

            return new CageCompatibilityViewModel
            {
                JaulaId = j.Id,
                CodigoJaula = j.Codigo.Trim(),
                Compatible = compatible,
                Motivo = compatible ? "Compatible" : "La mezcla de dietas incumple la regla del ejercicio.",
                Animales = animales
            };
        })];
        }

        public static AnimalDietType InferDiet(string especie)
        {
            var text = NormalizeText(especie);

            if (ContainsAny(text, "vaca", "oveja", "caballo", "yegua", "burro", "jirafa", "cebra", "elefante", "ciervo", "conejo", "tortuga", "hipopotamo", "hipopot", "rinoceronte", "camello", "dromedario", "capibara", "pato"))
                return AnimalDietType.Herbivoro;

            if (ContainsAny(text, "leon", "tigre", "lobo", "aguila", "serpiente", "cobra", "piton", "cocodrilo", "hiena", "pantera", "guepardo", "jaguar", "caiman"))
                return AnimalDietType.Carnivoro;

            if (ContainsAny(text, "oso", "cerdo", "chimpance", "mapache", "gallina", "mono"))
                return AnimalDietType.Omnivoro;

            return AnimalDietType.Desconocida;
        }

        private static bool ContainsAny(string text, params string[] values)
            => values.Any(v => text.Contains(v));

        private static string NormalizeText(string value)
        {
            var normalized = value.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);
            var chars = normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars);
        }

        private static AnimalDietType ParseDiet(string dieta)
            => Enum.TryParse<AnimalDietType>(dieta, out var result) ? result : AnimalDietType.Desconocida;

        private static bool IsCompatible(List<AnimalDietType> dietas)
        {
            var hasHerbivoro = dietas.Contains(AnimalDietType.Herbivoro);
            var hasCarnivoro = dietas.Contains(AnimalDietType.Carnivoro);
            var hasOmnivoro = dietas.Contains(AnimalDietType.Omnivoro);

            if (hasCarnivoro && hasHerbivoro)
                return false;

            if (hasOmnivoro && hasHerbivoro)
                return false;

            return true;
        }
    }
}
