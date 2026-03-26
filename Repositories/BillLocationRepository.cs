using AfiErpSystem.Controllers;
using AfiErpSystem.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AfiErpSystem.Repositories
{
    internal class BillLocationRepository
    {
        public int AddBillLocation(BillLocation billLocation)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", billLocation.Name);
                parameters.Add("@Address", billLocation.Address);

                int newId = con.ExecuteScalar<int>(
                    "sp_InsertBillLocation",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                return newId;
            }
        }

        public IEnumerable<BillLocation> GetBillLocations(int? billLocationId = null)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BillLocationId", billLocationId);

                return con.Query<BillLocation>(
                    "sp_GetBillLocations",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}