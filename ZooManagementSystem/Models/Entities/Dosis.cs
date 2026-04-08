namespace ZooManagementSystem.Models.Entities
{
    public class Dosis
    {
        public int DosisId { get; set; }
        public int? AnimalId { get; set; }
        public int? AlimentoId { get; set; }
        public int? Cantidad { get; set; }

        public Animal? Animal { get; set; }
        public Alimento? Alimento { get; set; }
    }
}
