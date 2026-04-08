using ZooManagementSystem.Models.ViewModels;

namespace ZooManagementSystem.Services.Interfaces
{
    public interface ICompatibilityService
    {
        Task<List<CageCompatibilityViewModel>> GetCageCompatibilityAsync(CancellationToken cancellationToken = default);
    }
}
