namespace ZooManagementSystem.Models.ViewModels
{
    public class ManagementDashboardViewModel
    {
        public decimal InversionTotalComida { get; set; }
        public List<AnimalCostViewModel> CostePorAnimal { get; set; } = new();
    }
}
