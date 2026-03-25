using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfiErpSystem.Models
{
    public class BusinessPartner
    {
        public int BPId { get; set; }
        public string BPName { get; set; }
        public string BPType { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
