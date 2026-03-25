using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfiErpSystem.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        // Identifiers
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ShortDescription { get; set; }
        public string Barcode { get; set; }
        public string ManufacturerPartNo { get; set; }

        // Classification
        public string ItemType { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }

        // UOM
        public string BaseUoM { get; set; }

        // Inventory
        public bool IsBatchManaged { get; set; }
        public bool IsSerialManaged { get; set; }

        // Pricing
        public decimal? CostPrice { get; set; }
        public decimal? SellingPrice { get; set; }
        public string Currency { get; set; }
        public string TaxCategory { get; set; }

        // Purchasing
        public int? PreferredSupplierId { get; set; }
        public string SupplierName { get; set; }
        public int? LeadTimeDays { get; set; }
        public int? MOQ { get; set; }
        public string VendorCatalogNumber { get; set; }

        // Planning
        public int? SafetyStock { get; set; }
        public int? ReorderPoint { get; set; }

        // Physical
        public decimal? Weight { get; set; }
        public string Dimensions { get; set; }
        public string Color { get; set; }

        // Accounting
        public string GLAccount { get; set; }
        public string CostingMethod { get; set; }

        // Status
        public string Status { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }

        // Audit
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
