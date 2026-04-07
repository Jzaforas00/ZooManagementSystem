namespace ZooManagementSystem.Models.Entities
{
    public class Animal
    {
        public int Id { get; set; }
        public string? Especie { get; set; }
        public string? NombrePopular { get; set; }
        public int EcosistemaId { get; set; }
        public string? UrlImagen { get; set; }
        public int JaulaId { get; set; }
        public int? AlimentacionId { get; set; }

        public Alimentacion? Alimentacion { get; set; }
        public Jaula? Jaula { get; set; }
        public Ecosistema? Ecosistema { get; set; }
        public ICollection<Dosis> Dosis { get; set; } = new List<Dosis>();
    }
}
