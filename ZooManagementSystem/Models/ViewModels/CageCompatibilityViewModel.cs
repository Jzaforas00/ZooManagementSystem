namespace ZooManagementSystem.Models.ViewModels
{
    public class CageCompatibilityViewModel
    {
        public int JaulaId { get; set; }
        public string CodigoJaula { get; set; } = string.Empty;
        public bool Compatible { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public List<AnimalInCageViewModel> Animales { get; set; } = new();
    }
}
