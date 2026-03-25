using AfiErpSystem.Controllers;
using AfiErpSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfiErpSystem.Repositories
{
    internal class BusinessPartnerRepository
    {
        public int AddBusinessPartner(BusinessPartner bp)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BPName", bp.BPName);
                parameters.Add("@BPType", bp.BPType);
                parameters.Add("@Phone", bp.Phone);
                parameters.Add("@Email", bp.Email);
                parameters.Add("@Address", bp.Address);

                int newId = con.ExecuteScalar<int>(
                    "sp_InsertBusinessPartner",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                return newId;
            }
        }

        public IEnumerable<BusinessPartner> GetBusinessPartners(int? bpId = null, string bpType = null)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BPId", bpId);
                parameters.Add("@BPType", bpType);

                return con.Query<BusinessPartner>(
                    "sp_GetBusinessPartners",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}
