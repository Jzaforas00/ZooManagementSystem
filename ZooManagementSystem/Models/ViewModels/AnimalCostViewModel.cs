namespace ZooManagementSystem.Models.ViewModels
{
    public class AnimalCostViewModel
    {
        public int AnimalId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Especie { get; set; } = string.Empty;
        public decimal Coste { get; set; }
    }
}
