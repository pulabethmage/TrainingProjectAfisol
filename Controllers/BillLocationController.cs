using AfiErpSystem.Models;
using AfiErpSystem.Repositories;
using System;
using System.Collections.Generic;

namespace AfiErpSystem.Controllers
{
    public class BillLocationController
    {
        private readonly BillLocationRepository _repo;

        public BillLocationController()
        {
            _repo = new BillLocationRepository();
        }

        public int Save(BillLocation billLocation)
        {
            // Validation
            if (string.IsNullOrEmpty(billLocation.Name))
                throw new Exception("Bill Location Name is required.");

            return _repo.AddBillLocation(billLocation);
        }

        public IEnumerable<BillLocation> GetAll()
        {
            return _repo.GetBillLocations();
        }

        public BillLocation GetById(int billLocationId)
        {
            var results = _repo.GetBillLocations(billLocationId: billLocationId);
            foreach (var bl in results) return bl;
            return null;
        }
    }
}