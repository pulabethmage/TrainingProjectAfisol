using AfiErpSystem.Controllers;
using AfiErpSystem.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AfiErpSystem.Repositories
{
    internal class LocationRepository
    {
        public int AddLocation(Location location)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@WarehouseName", location.WarehouseName);
                parameters.Add("@Rack", location.Rack);
                parameters.Add("@Cell", location.Cell);
                parameters.Add("@Stack", location.Stack);

                int newId = con.ExecuteScalar<int>(
                    "sp_InsertLocation",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                return newId;
            }
        }

        public IEnumerable<Location> GetLocations(int? locationId = null, string warehouseName = null)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@LocationId", locationId);
                parameters.Add("@WarehouseName", warehouseName);

                return con.Query<Location>(
                    "sp_GetLocations",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}