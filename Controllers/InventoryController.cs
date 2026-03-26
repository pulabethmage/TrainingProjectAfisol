using AfiErpSystem.Models;
using AfiErpSystem.Repositories;
using System.Collections.Generic;

namespace AfiErpSystem.Controllers
{
    public class InventoryController
    {
        private readonly InventoryRepository _repo;

        public InventoryController()
        {
            _repo = new InventoryRepository();
        }

        public IEnumerable<InventoryView> GetAll()
        {
            return _repo.GetInventory();
        }

        public IEnumerable<InventoryView> GetByLocation(int locationId)
        {
            return _repo.GetInventory(locationId: locationId);
        }

        public IEnumerable<InventoryView> Search(string itemName)
        {
            return _repo.GetInventory(itemName: itemName);
        }

        public IEnumerable<InventoryView> GetActiveOnly()
        {
            return _repo.GetInventory(status: "Active");
        }
    }
}