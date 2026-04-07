namespace ZooManagementSystem.Models.Entities
{
    public class CategoriaLaboral
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Sueldo { get; set; }

        public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}
