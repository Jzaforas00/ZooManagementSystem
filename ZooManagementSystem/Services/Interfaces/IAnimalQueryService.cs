using ZooManagementSystem.Models.ViewModels;

namespace ZooManagementSystem.Services.Interfaces
{
    // Service to get animal data for the UI
    public interface IAnimalQueryService
    {
        // Gets a list of animal cards, optionally filtered by species
        Task<List<AnimalCardViewModel>> GetAnimalCardsAsync(
            string? especie = null, 
            CancellationToken cancellationToken = default);
    }
}
