using System;

namespace AfiErpSystem.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string WarehouseName { get; set; }
        public string Rack { get; set; }
        public string Cell { get; set; }
        public string Stack { get; set; }
    }
}