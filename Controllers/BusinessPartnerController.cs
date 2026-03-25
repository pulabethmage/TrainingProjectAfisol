using AfiErpSystem.Models;
using AfiErpSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfiErpSystem.Controllers
{
    public class BusinessPartnerController
    {
        private readonly BusinessPartnerRepository _repo;

        public BusinessPartnerController()
        {
            _repo = new BusinessPartnerRepository();
        }

        public int Save(BusinessPartner bp)
        {
            // Validation
            if (string.IsNullOrEmpty(bp.BPName))
                throw new Exception("Business Partner Name is required.");

            if (string.IsNullOrEmpty(bp.BPType))
                throw new Exception("Business Partner Type is required.");

            if (bp.BPType != "Customer" && bp.BPType != "Supplier" && bp.BPType != "Both")
                throw new Exception("BPType must be Customer, Supplier, or Both.");

            if (!string.IsNullOrEmpty(bp.Email) && !bp.Email.Contains("@"))
                throw new Exception("Invalid email address.");

            return _repo.AddBusinessPartner(bp);
        }

        public IEnumerable<BusinessPartner> GetAll()
        {
            return _repo.GetBusinessPartners();
        }

        public IEnumerable<BusinessPartner> GetByType(string bpType)
        {
            return _repo.GetBusinessPartners(bpType: bpType);
        }

        public BusinessPartner GetById(int bpId)
        {
            var results = _repo.GetBusinessPartners(bpId: bpId);
            foreach (var bp in results) return bp; // return first
            return null;
        }
    }
}
