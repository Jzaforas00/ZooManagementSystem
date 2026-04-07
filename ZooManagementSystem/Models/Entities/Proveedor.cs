namespace ZooManagementSystem.Models.Entities
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }

        public ICollection<Alimento> Alimentos { get; set; } = new List<Alimento>();
    }
}
