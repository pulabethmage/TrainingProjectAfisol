using AfiErpSystem.Controllers;
using AfiErpSystem.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AfiErpSystem.Repositories
{
    internal class InventoryRepository
    {
        public IEnumerable<InventoryView> GetInventory(
            int? itemId = null, int? locationId = null,
            string itemName = null, string status = null)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@ItemId", itemId);
                p.Add("@LocationId", locationId);
                p.Add("@ItemName", itemName);
                p.Add("@Status", status);

                return con.Query<InventoryView>(
                    "sp_GetInventory", p,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}