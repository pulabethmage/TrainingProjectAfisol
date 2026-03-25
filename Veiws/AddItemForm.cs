using AfiErpSystem.Controllers;
using AfiErpSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AfiErpSystem.Veiws
{
    public partial class AddItemForm : Form
    {
        public AddItemForm()
        {
            InitializeComponent();
            this.Shown += AddItemForm_Shown;
        }

        private void AddItemForm_Load(object sender, EventArgs e)
        {
           
        }

        private void AddItemForm_Shown(object sender, EventArgs e)
        {
            LoadItems();           //  Grid inside panel — fully rendered now
            LoadBusinessPartners(); //  Combobox inside TabControl — fully rendered now
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // FIX 2 & 4: Use TryParse for all numeric fields — safe + culture-aware
                decimal? costPrice = null, sellingPrice = null, weight = null;
                int? leadTime = null, moq = null, safetyStock = null, reorderPoint = null;

                if (!string.IsNullOrEmpty(txtCost.Text))
                {
                    if (!decimal.TryParse(txtCost.Text, out decimal c))
                        throw new Exception("Invalid value for Cost Price.");
                    costPrice = c;
                }
                if (!string.IsNullOrEmpty(txtSell.Text))
                {
                    if (!decimal.TryParse(txtSell.Text, out decimal s))
                        throw new Exception("Invalid value for Selling Price.");
                    sellingPrice = s;
                }
                if (!string.IsNullOrEmpty(txtWeight.Text))
                {
                    if (!decimal.TryParse(txtWeight.Text, out decimal w))
                        throw new Exception("Invalid value for Weight.");
                    weight = w;
                }
                if (!string.IsNullOrEmpty(txtLeadTime.Text))
                {
                    if (!int.TryParse(txtLeadTime.Text, out int lt))
                        throw new Exception("Invalid value for Lead Time Days.");
                    leadTime = lt;
                }
                if (!string.IsNullOrEmpty(txtMOQ.Text))
                {
                    if (!int.TryParse(txtMOQ.Text, out int m))
                        throw new Exception("Invalid value for MOQ.");
                    moq = m;
                }
                if (!string.IsNullOrEmpty(txtSafety.Text))
                {
                    if (!int.TryParse(txtSafety.Text, out int ss))
                        throw new Exception("Invalid value for Safety Stock.");
                    safetyStock = ss;
                }
                if (!string.IsNullOrEmpty(txtReorder.Text))
                {
                    if (!int.TryParse(txtReorder.Text, out int rp))
                        throw new Exception("Invalid value for Reorder Point.");
                    reorderPoint = rp;
                }

                // FIX 1: Use explicit int cast after checking SelectedValue type
                int? supplierId = null;
                if (cmbSupplier.SelectedValue is int sid)
                    supplierId = sid;

                // FIX 3: Use Checked property for nullable DateTimePickers
                // (Requires ShowCheckBox = true on both dtpFrom and dtpTo in designer)
                DateTime? activeFrom = dtpFrom.Checked ? dtpFrom.Value : (DateTime?)null;
                DateTime? activeTo = dtpTo.Checked ? dtpTo.Value : (DateTime?)null;

                Item item = new Item
                {
                    ItemCode = txtItemCode.Text,
                    ItemName = txtItemName.Text,
                    ShortDescription = txtDesc.Text,
                    Barcode = txtBarcode.Text,
                    ManufacturerPartNo = txtMpn.Text,
                    ItemType = cmbType.Text,
                    Category = cmbCategory.Text,
                    SubCategory = cmbSubCategory.Text,
                    BaseUoM = cmbUom.Text,
                    IsBatchManaged = chkBatch.Checked,
                    IsSerialManaged = chkSerial.Checked,
                    CostPrice = costPrice,
                    SellingPrice = sellingPrice,
                    Currency = cmbCurrency.Text,
                    TaxCategory = cmbTax.Text,
                    PreferredSupplierId = supplierId,       // FIX 1
                    LeadTimeDays = leadTime,          // FIX 2
                    MOQ = moq,               // FIX 2
                    VendorCatalogNumber = txtVendor.Text,
                    SafetyStock = safetyStock,       // FIX 2
                    ReorderPoint = reorderPoint,      // FIX 2
                    Weight = weight,
                    Dimensions = txtDimensions.Text,
                    Color = txtColor.Text,
                    GLAccount = txtGL.Text,
                    CostingMethod = cmbCosting.Text,
                    Status = cmbStatus.Text,
                    ActiveFrom = activeFrom,        // FIX 3
                    ActiveTo = activeTo,          // FIX 3
                    CreatedBy = "Admin"
                };

                ItemController controller = new ItemController();
                controller.Save(item);
                MessageBox.Show("Item saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadItems();
                LoadBusinessPartners();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void LoadBusinessPartners()
        {
            try
            {
                BusinessPartnerController bpController = new BusinessPartnerController();
                var suppliers = bpController.GetByType("Supplier").ToList();

                cmbSupplier.DataSource = null;        // Clear first
                cmbSupplier.DataSource = suppliers;
                cmbSupplier.DisplayMember = "BPName";
                cmbSupplier.ValueMember = "BPId";

                if (suppliers.Count > 0)
                    cmbSupplier.SelectedIndex = -1;      // Only set if list has items
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }




        private void LoadItems()
        {
            try
            {
                ItemController controller = new ItemController();
                dataGridView1.DataSource = controller.GetAll().ToList();

                // Safe column header setting — won't crash if column missing
                void SetHeader(string col, string header)
                {
                    if (dataGridView1.Columns.Contains(col))
                        dataGridView1.Columns[col].HeaderText = header;
                }

                SetHeader("ItemId", "ID");
                SetHeader("ItemCode", "Item Code");
                SetHeader("ItemName", "Item Name");
                SetHeader("ShortDescription", "Description");
                SetHeader("Barcode", "Barcode");
                SetHeader("ManufacturerPartNo", "MPN");
                SetHeader("ItemType", "Type");
                SetHeader("Category", "Category");
                SetHeader("SubCategory", "Sub Category");
                SetHeader("BaseUoM", "UoM");
                SetHeader("IsBatchManaged", "Batch");
                SetHeader("IsSerialManaged", "Serial");
                SetHeader("CostPrice", "Cost Price");
                SetHeader("SellingPrice", "Selling Price");
                SetHeader("Currency", "Currency");
                SetHeader("TaxCategory", "Tax");
                SetHeader("PreferredSupplierId", "Supplier ID");
                SetHeader("SupplierName", "Supplier");   // new
                SetHeader("LeadTimeDays", "Lead Time");
                SetHeader("MOQ", "MOQ");
                SetHeader("VendorCatalogNumber", "Vendor Cat No");
                SetHeader("SafetyStock", "Safety Stock");
                SetHeader("ReorderPoint", "Reorder Point");
                SetHeader("Weight", "Weight");
                SetHeader("Dimensions", "Dimensions");
                SetHeader("Color", "Color");
                SetHeader("GLAccount", "GL Account");
                SetHeader("CostingMethod", "Costing Method");
                SetHeader("Status", "Status");
                SetHeader("ActiveFrom", "Active From");
                SetHeader("ActiveTo", "Active To");
                SetHeader("CreatedDate", "Created Date");
                SetHeader("CreatedBy", "Created By");
                SetHeader("UpdatedDate", "Updated Date");
                SetHeader("UpdatedBy", "Updated By");

                // Appearance
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
    }
}
