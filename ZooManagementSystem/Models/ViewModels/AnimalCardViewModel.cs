namespace ZooManagementSystem.Models.ViewModels
{
    public class AnimalCardViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Especie { get; set; } = string.Empty;
        public string Ecosistema { get; set; } = string.Empty;
        public string Dieta { get; set; } = string.Empty;
        public string? FotoUrl { get; set; }
        public string Jaula { get; set; } = string.Empty;
        public string Zona { get; set; } = string.Empty;
    }
}
