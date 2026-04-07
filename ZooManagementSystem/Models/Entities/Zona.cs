namespace ZooManagementSystem.Models.Entities
{
    public class Zona
    {
        public int ZonaId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int? EcosistemaId { get; set; }

        public Ecosistema? Ecosistema { get; set; }
        public ICollection<Jaula> Jaulas { get; set; } = new List<Jaula>();
    }
}
