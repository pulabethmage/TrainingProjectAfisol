using AfiErpSystem.Models;
using AfiErpSystem.Repositories;
using System;
using System.Collections.Generic;

namespace AfiErpSystem.Controllers
{
    public class GRNController
    {
        private readonly GRNRepository _repo;

        public GRNController()
        {
            _repo = new GRNRepository();
        }

        public int Save(GRN grn)
        {
            if (string.IsNullOrEmpty(grn.GRNNumber))
                throw new Exception("GRN Number is required.");
            if (grn.SupplierId <= 0)
                throw new Exception("Supplier is required.");
            if (grn.BillLocationId <= 0)
                throw new Exception("Bill Location is required.");

            return _repo.AddGRN(grn);
        }

        public int AddLine(GRNLine line)
        {
            if (line.GRNId <= 0)
                throw new Exception("GRN must be saved before adding lines.");
            if (line.ItemId <= 0)
                throw new Exception("Item is required.");
            if (line.LocationId <= 0)
                throw new Exception("Location is required.");
            if (line.Quantity <= 0)
                throw new Exception("Quantity must be greater than zero.");

            return _repo.AddGRNLine(line);
        }

        public void Approve(int grnId)
        {
            if (grnId <= 0)
                throw new Exception("Invalid GRN.");

            _repo.ApproveGRN(grnId);
        }

        public IEnumerable<GRN> GetAll() => _repo.GetGRNs();
        public IEnumerable<GRN> GetPending() => _repo.GetGRNs(status: "Pending");
        public IEnumerable<GRNLine> GetLines(int grnId) => _repo.GetGRNLines(grnId);
        public GRN GetById(int grnId)
        {
            foreach (var g in _repo.GetGRNs(grnId: grnId)) return g;
            return null;
        }
    }
}