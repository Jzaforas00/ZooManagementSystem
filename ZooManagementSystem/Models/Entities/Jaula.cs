namespace ZooManagementSystem.Models.Entities
{
    public class Jaula
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public int? ZonaId { get; set; }

        public Zona? Zona { get; set; }
        public ICollection<Animal> Animales { get; set; } = new List<Animal>();
        public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}
