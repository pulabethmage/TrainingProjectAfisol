using AfiErpSystem.Controllers;
using AfiErpSystem.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AfiErpSystem.Repositories
{
    internal class InventoryTransferRepository
    {
        public int AddTransfer(InventoryTransfer transfer)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@TransferNumber", transfer.TransferNumber);
                p.Add("@FromLocationId", transfer.FromLocationId);
                p.Add("@ToLocationId", transfer.ToLocationId);
                p.Add("@TransferDate", transfer.TransferDate);

                return con.ExecuteScalar<int>(
                    "sp_InsertInventoryTransfer", p,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public int AddTransferLine(InventoryTransferLine line)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@TransferId", line.TransferId);
                p.Add("@ItemId", line.ItemId);
                p.Add("@Quantity", line.Quantity);
                p.Add("@BatchId", line.BatchId);
                p.Add("@SerialId", line.SerialId);

                return con.ExecuteScalar<int>(
                    "sp_InsertInventoryTransferLine", p,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void ApproveTransfer(int transferId)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@TransferId", transferId);

                con.Execute(
                    "sp_ApproveInventoryTransfer", p,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<InventoryTransfer> GetTransfers(
            int? transferId = null, string status = null)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@TransferId", transferId);
                p.Add("@Status", status);

                return con.Query<InventoryTransfer>(
                    "sp_GetInventoryTransfers", p,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<InventoryTransferLine> GetTransferLines(int transferId)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@TransferId", transferId);

                return con.Query<InventoryTransferLine>(
                    "sp_GetInventoryTransferLines", p,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}