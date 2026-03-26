namespace AfiErpSystem.Models
{
    public class GRNLine
    {
        public int GRNLineId { get; set; }
        public int GRNId { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public int LocationId { get; set; }
        public string WarehouseName { get; set; }
        public string Rack { get; set; }
        public string Cell { get; set; }
        public string Stack { get; set; }
        public int? BatchId { get; set; }
        public int? SerialId { get; set; }
    }
}