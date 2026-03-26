using AfiErpSystem.Models;
using AfiErpSystem.Repositories;
using System;
using System.Collections.Generic;

namespace AfiErpSystem.Controllers
{
    public class InventoryTransferController
    {
        private readonly InventoryTransferRepository _repo;

        public InventoryTransferController()
        {
            _repo = new InventoryTransferRepository();
        }

        public int Save(InventoryTransfer transfer)
        {
            if (string.IsNullOrEmpty(transfer.TransferNumber))
                throw new Exception("Transfer Number is required.");
            if (transfer.FromLocationId <= 0)
                throw new Exception("From Location is required.");
            if (transfer.ToLocationId <= 0)
                throw new Exception("To Location is required.");
            if (transfer.FromLocationId == transfer.ToLocationId)
                throw new Exception("From and To locations cannot be the same.");

            return _repo.AddTransfer(transfer);
        }

        public int AddLine(InventoryTransferLine line)
        {
            if (line.ItemId <= 0)
                throw new Exception("Item is required.");
            if (line.Quantity <= 0)
                throw new Exception("Quantity must be greater than zero.");

            return _repo.AddTransferLine(line);
        }

        public void Approve(int transferId)
        {
            if (transferId <= 0)
                throw new Exception("Invalid Transfer.");

            _repo.ApproveTransfer(transferId);
        }

        public IEnumerable<InventoryTransfer> GetAll() => _repo.GetTransfers();
        public IEnumerable<InventoryTransfer> GetPending() => _repo.GetTransfers(status: "Pending");
        public IEnumerable<InventoryTransferLine> GetLines(int transferId)
            => _repo.GetTransferLines(transferId);
    }
}