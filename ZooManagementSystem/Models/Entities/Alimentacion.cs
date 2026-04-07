namespace ZooManagementSystem.Models.Entities
{
    public class Alimentacion
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public ICollection<Animal> Animales { get; set; } = new List<Animal>();
    }
}
