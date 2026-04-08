using ZooManagementSystem.Models.ViewModels;

namespace ZooManagementSystem.Services.Interfaces
{
    public interface IManagementService
    {
        Task<List<AnimalCostViewModel>> GetAnimalCostsAsync(CancellationToken cancellationToken = default);
        Task<decimal> GetFoodInvestmentAsync(CancellationToken cancellationToken = default);
    }
}
