namespace ZooManagementSystem.Models.Entities
{
    public class Ecosistema
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public string? Url { get; set; }

        public ICollection<Zona> Zonas { get; set; } = new List<Zona>();
    }
}
