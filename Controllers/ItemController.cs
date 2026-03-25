using AfiErpSystem.Models;
using AfiErpSystem.Repositories;
using System;
using System.Collections.Generic;

namespace AfiErpSystem.Controllers
{
    public class ItemController
    {
        private readonly ItemRepository _repo;

        public ItemController()
        {
            // FIX 3: ItemRepository takes no constructor arguments — removed
            // DbConfig.ConnectionString argument and extra closing parenthesis
            _repo = new ItemRepository();
        }

        public void Save(Item item)
        {
            if (string.IsNullOrEmpty(item.ItemCode))
                throw new Exception("Item Code required");
            if (string.IsNullOrEmpty(item.ItemName))
                throw new Exception("Item Name required");

            _repo.AddItem(item);
        }


        public IEnumerable<Item> GetAll()
        {
            return _repo.GetItems();
        }

        public IEnumerable<Item> GetActiveItems()
        {
            return _repo.GetItems(status: "Active");
        }

        public Item GetById(int itemId)
        {
            var results = _repo.GetItems(itemId: itemId);
            foreach (var item in results) return item;
            return null;
        }

        public IEnumerable<Item> Search(string itemName)
        {
            return _repo.GetItems(itemName: itemName);
        }




    }
}