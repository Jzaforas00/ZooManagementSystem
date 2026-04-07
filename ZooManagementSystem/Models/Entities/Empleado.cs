namespace ZooManagementSystem.Models.Entities
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Apodo { get; set; }
        public string? Telefono { get; set; }
        public string? Cp { get; set; }
        public int? CategoriasId { get; set; }
        public int? JaulaId { get; set; }

        public CategoriaLaboral? Categoria { get; set; }
        public Jaula? Jaula { get; set; }
    }
}
