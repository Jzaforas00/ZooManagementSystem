namespace ZooManagementSystem.Models.ViewModels
{
    public class AnimalInCageViewModel
    {
        public int AnimalId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Especie { get; set; } = string.Empty;
        public string Dieta { get; set; } = string.Empty;
    }
}
