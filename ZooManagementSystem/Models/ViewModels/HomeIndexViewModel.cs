namespace ZooManagementSystem.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public string? EspecieFiltro { get; set; }
        public List<string> EspeciesDisponibles { get; set; } = new();
        public List<AnimalCardViewModel> Animales { get; set; } = new();
    }
}
