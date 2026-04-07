namespace ZooManagementSystem.Models.Entities
{
    public class Alimento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public int? ProveedorId { get; set; }
        public string? Url { get; set; }
        public int? Calorias { get; set; }

        public Proveedor? Proveedor { get; set; }
        public ICollection<Dosis> Dosis { get; set; } = new List<Dosis>();
    }
}
