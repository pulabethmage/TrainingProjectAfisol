using AfiErpSystem.Controllers;
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
    public partial class FormInventory : Form
    {
        public FormInventory()
        {
            InitializeComponent();
        }

        private void FormInventory_Load(object sender, EventArgs e)
        {
            LoadInventory();
        }





        private void LoadInventory(string searchText = null)
        {
            try
            {
                var controller = new InventoryController();
                var inventory = string.IsNullOrEmpty(searchText)
                    ? controller.GetAll().ToList()
                    : controller.Search(searchText).ToList();

                dataGridView1.DataSource = null;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "ItemCode", HeaderText = "Item Code", DataPropertyName = "ItemCode", Width = 100 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "ItemName", HeaderText = "Item Name", DataPropertyName = "ItemName", Width = 160 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "Category", HeaderText = "Category", DataPropertyName = "Category", Width = 100 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "Quantity", HeaderText = "Qty", DataPropertyName = "Quantity", Width = 70 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "BaseUoM", HeaderText = "UoM", DataPropertyName = "BaseUoM", Width = 60 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "WarehouseName", HeaderText = "Warehouse", DataPropertyName = "WarehouseName", Width = 120 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "Rack", HeaderText = "Rack", DataPropertyName = "Rack", Width = 50 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "Cell", HeaderText = "Cell", DataPropertyName = "Cell", Width = 50 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "Stack", HeaderText = "Stack", DataPropertyName = "Stack", Width = 50 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "SafetyStock", HeaderText = "Safety Stk", DataPropertyName = "SafetyStock", Width = 80 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "ReorderPoint", HeaderText = "Reorder Pt", DataPropertyName = "ReorderPoint", Width = 80 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "StockAlert", HeaderText = "Alert", DataPropertyName = "StockAlert", Width = 100 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "UpdatedDate", HeaderText = "Last Updated", DataPropertyName = "UpdatedDate", Width = 120 });

                dataGridView1.DataSource = inventory;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                //  Color rows based on StockAlert
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string alert = row.Cells["StockAlert"].Value?.ToString();
                    switch (alert)
                    {
                        case "Out of Stock":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200); // Red
                            row.DefaultCellStyle.ForeColor = Color.DarkRed;
                            break;
                        case "Below Safety":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 180); // Orange
                            row.DefaultCellStyle.ForeColor = Color.DarkOrange;
                            break;
                        case "Reorder Soon":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // Yellow
                            row.DefaultCellStyle.ForeColor = Color.DarkGoldenrod;
                            break;
                        default:
                            row.DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220); // Green
                            row.DefaultCellStyle.ForeColor = Color.DarkGreen;
                            break;
                    }
                }

                //  Update summary label
                lblSummary.Text =
                    $"Total Items: {inventory.Count}   |   " +
                    $"Out of Stock: {inventory.Count(x => x.StockAlert == "Out of Stock")}   |   " +
                    $"Below Safety: {inventory.Count(x => x.StockAlert == "Below Safety")}   |   " +
                    $"Reorder Soon: {inventory.Count(x => x.StockAlert == "Reorder Soon")}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadInventory(txtSearch.Text);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadInventory();
        }
    }
}
