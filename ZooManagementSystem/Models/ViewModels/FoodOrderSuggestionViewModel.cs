namespace ZooManagementSystem.Models.ViewModels
{
    public class FoodOrderSuggestionViewModel
    {
        public int AlimentoId { get; set; }
        public string Alimento { get; set; } = string.Empty;
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public int DosisNecesarias { get; set; }
        public int CantidadAPedir { get; set; }
        public string Proveedor { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }
    }
}
