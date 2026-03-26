using AfiErpSystem.Models;
using AfiErpSystem.Repositories;
using System;
using System.Collections.Generic;

namespace AfiErpSystem.Controllers
{
    public class LocationController
    {
        private readonly LocationRepository _repo;

        public LocationController()
        {
            _repo = new LocationRepository();
        }

        public int Save(Location location)
        {
            // Validation
            if (string.IsNullOrEmpty(location.WarehouseName))
                throw new Exception("Warehouse Name is required.");

            return _repo.AddLocation(location);
        }

        public IEnumerable<Location> GetAll()
        {
            return _repo.GetLocations();
        }

        public IEnumerable<Location> GetByWarehouse(string warehouseName)
        {
            return _repo.GetLocations(warehouseName: warehouseName);
        }

        public Location GetById(int locationId)
        {
            var results = _repo.GetLocations(locationId: locationId);
            foreach (var loc in results) return loc;
            return null;
        }
    }
}