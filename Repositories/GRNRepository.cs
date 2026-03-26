using AfiErpSystem.Controllers;
using AfiErpSystem.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AfiErpSystem.Repositories
{
    internal class GRNRepository
    {
        // HEADERS
        public int AddGRN(GRN grn)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@GRNNumber", grn.GRNNumber);
                p.Add("@SupplierId", grn.SupplierId);
                p.Add("@GRNDate", grn.GRNDate);
                p.Add("@BillLocationId", grn.BillLocationId);
                p.Add("@Remark", grn.Remark);

                return con.ExecuteScalar<int>(
                    "sp_InsertGRN", p,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<GRN> GetGRNs(int? grnId = null, string status = null)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@GRNId", grnId);
                p.Add("@Status", status);

                return con.Query<GRN>(
                    "sp_GetGRNs", p,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void ApproveGRN(int grnId)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@GRNId", grnId);

                con.Execute(
                    "sp_ApproveGRN", p,
                    commandType: CommandType.StoredProcedure);
            }
        }

        // LINES
        public int AddGRNLine(GRNLine line)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@GRNId", line.GRNId);
                p.Add("@ItemId", line.ItemId);
                p.Add("@Quantity", line.Quantity);
                p.Add("@LocationId", line.LocationId);
                p.Add("@BatchId", line.BatchId);
                p.Add("@SerialId", line.SerialId);

                return con.ExecuteScalar<int>(
                    "sp_InsertGRNLine", p,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<GRNLine> GetGRNLines(int grnId)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@GRNId", grnId);

                return con.Query<GRNLine>(
                    "sp_GetGRNLines", p,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}