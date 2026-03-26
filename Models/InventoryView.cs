using System;

namespace AfiErpSystem.Models
{
    public class InventoryView
    {
        public int InventoryId { get; set; }
        // Item
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public string BaseUoM { get; set; }
        // Stock
        public decimal Quantity { get; set; }
        // Location
        public string WarehouseName { get; set; }
        public string Rack { get; set; }
        public string Cell { get; set; }
        public string Stack { get; set; }
        // Alerts
        public int? SafetyStock { get; set; }
        public int? ReorderPoint { get; set; }
        public string StockAlert { get; set; }
        // Batch / Serial
        public int? BatchId { get; set; }
        public int? SerialId { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}