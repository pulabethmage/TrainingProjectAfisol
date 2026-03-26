using System;

namespace AfiErpSystem.Models
{
    public class GRN
    {
        public int GRNId { get; set; }
        public string GRNNumber { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime GRNDate { get; set; }
        public string Status { get; set; }
        public int BillLocationId { get; set; }
        public string BillLocationName { get; set; }
        public string Remark { get; set; }
    }
}