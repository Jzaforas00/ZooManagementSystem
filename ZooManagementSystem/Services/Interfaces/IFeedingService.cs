using ZooManagementSystem.Models.ViewModels;

namespace ZooManagementSystem.Services.Interfaces
{
    public interface IFeedingService
    {
        Task<List<FoodOrderSuggestionViewModel>> GetFoodOrderSuggestionsAsync(CancellationToken cancellationToken = default);
        Task<int> GetCaloriesByAnimalAsync(int animalId, CancellationToken cancellationToken = default);
    }
}
