using System;

namespace AfiErpSystem.Models
{
    public class InventoryTransfer
    {
        public int TransferId { get; set; }
        public string TransferNumber { get; set; }
        public int FromLocationId { get; set; }
        public string FromLocation { get; set; }
        public int ToLocationId { get; set; }
        public string ToLocation { get; set; }
        public DateTime TransferDate { get; set; }
        public string Status { get; set; }
    }

    public class InventoryTransferLine
    {
        public int TransferLineId { get; set; }
        public int TransferId { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public int? BatchId { get; set; }
        public int? SerialId { get; set; }
    }
}