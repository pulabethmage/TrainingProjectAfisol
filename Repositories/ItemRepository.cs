using AfiErpSystem.Controllers;
using AfiErpSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;


namespace AfiErpSystem.Repositories
{
    internal class ItemRepository
    {
        public int AddItem(Item item)
        {
            // FIX 1: Removed extra closing parenthesis
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ItemCode", item.ItemCode);
                parameters.Add("@ItemName", item.ItemName);
                parameters.Add("@ShortDescription", item.ShortDescription);
                parameters.Add("@Barcode", item.Barcode);
                parameters.Add("@ManufacturerPartNo", item.ManufacturerPartNo);
                parameters.Add("@ItemType", item.ItemType);
                parameters.Add("@Category", item.Category);
                parameters.Add("@SubCategory", item.SubCategory);
                parameters.Add("@BaseUoM", item.BaseUoM);
                parameters.Add("@IsBatchManaged", item.IsBatchManaged);
                parameters.Add("@IsSerialManaged", item.IsSerialManaged);
                parameters.Add("@CostPrice", item.CostPrice);
                parameters.Add("@SellingPrice", item.SellingPrice);
                parameters.Add("@Currency", item.Currency);
                parameters.Add("@TaxCategory", item.TaxCategory);
                parameters.Add("@PreferredSupplierId", item.PreferredSupplierId);
                parameters.Add("@LeadTimeDays", item.LeadTimeDays);
                parameters.Add("@MOQ", item.MOQ);
                parameters.Add("@VendorCatalogNumber", item.VendorCatalogNumber);
                parameters.Add("@SafetyStock", item.SafetyStock);
                parameters.Add("@ReorderPoint", item.ReorderPoint);
                parameters.Add("@Weight", item.Weight);
                parameters.Add("@Dimensions", item.Dimensions);
                parameters.Add("@Color", item.Color);
                parameters.Add("@GLAccount", item.GLAccount);
                parameters.Add("@CostingMethod", item.CostingMethod);
                parameters.Add("@Status", item.Status);
                parameters.Add("@ActiveFrom", item.ActiveFrom);
                parameters.Add("@ActiveTo", item.ActiveTo);
                parameters.Add("@CreatedBy", item.CreatedBy);

                int newId = con.ExecuteScalar<int>(
                    "sp_InsertItem",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                return newId;
            }
        } // FIX 2: Removed the extra stray closing brace that was here


        public IEnumerable<Item> GetItems(int? itemId = null, string itemCode = null,
                                   string itemName = null, string itemType = null,
                                   string category = null, string status = null)
        {
            using (var con = new SqlConnection(DbConfig.ConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ItemId", itemId);
                parameters.Add("@ItemCode", itemCode);
                parameters.Add("@ItemName", itemName);
                parameters.Add("@ItemType", itemType);
                parameters.Add("@Category", category);
                parameters.Add("@Status", status);

                return con.Query<Item>(
                    "sp_GetItems",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }






    }
}