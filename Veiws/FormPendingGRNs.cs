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
    public partial class FormPendingGRNs : Form
    {
        public FormPendingGRNs()
        {
            InitializeComponent();
        }

        private void FormPendingGRNs_Load(object sender, EventArgs e)
        {
            LoadPendingGRNs();
            SetupLinesGrid();
        }


        private void LoadPendingGRNs()
        {
            try
            {
                var controller = new GRNController();
                var pending = controller.GetPending().ToList();

                dataGridView1.DataSource = null;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "GRNId", HeaderText = "GRN ID", DataPropertyName = "GRNId", Width = 60 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "GRNNumber", HeaderText = "GRN Number", DataPropertyName = "GRNNumber", Width = 120 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "SupplierName", HeaderText = "Supplier", DataPropertyName = "SupplierName", Width = 150 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "GRNDate", HeaderText = "Date", DataPropertyName = "GRNDate", Width = 120 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "BillLocationName", HeaderText = "Bill Location", DataPropertyName = "BillLocationName", Width = 130 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "Status", HeaderText = "Status", DataPropertyName = "Status", Width = 80 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "Remark", HeaderText = "Remark", DataPropertyName = "Remark", Width = 150 });

                dataGridView1.DataSource = pending;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.RowHeadersVisible = false;

                this.Text = $"Pending GRNs ({pending.Count})";

                //  Clear lines grid when reloading
                dataGridView2.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Setup Lines Grid columns ──────────────────────────
        private void SetupLinesGrid()
        {
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.Columns.Clear();

            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "ItemCode", HeaderText = "Item Code", DataPropertyName = "ItemCode", Width = 100 });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "ItemName", HeaderText = "Item Name", DataPropertyName = "ItemName", Width = 180 });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "Quantity", HeaderText = "Quantity", DataPropertyName = "Quantity", Width = 80 });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "WarehouseName", HeaderText = "Warehouse", DataPropertyName = "WarehouseName", Width = 120 });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "Rack", HeaderText = "Rack", DataPropertyName = "Rack", Width = 60 });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "Cell", HeaderText = "Cell", DataPropertyName = "Cell", Width = 60 });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "Stack", HeaderText = "Stack", DataPropertyName = "Stack", Width = 60 });

            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.ReadOnly = true;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.RowHeadersVisible = false;
        }

        // ── When a GRN row is selected — load its lines ───────
       

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                    throw new Exception("Please select a GRN to approve.");

                var selectedRow = dataGridView1.SelectedRows[0];
                int grnId = Convert.ToInt32(selectedRow.Cells["GRNId"].Value);
                string grnNumber = selectedRow.Cells["GRNNumber"].Value.ToString();

                var confirm = MessageBox.Show(
                    $"Approve GRN: {grnNumber}?\nThis will update Inventory.",
                    "Confirm Approve",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                var controller = new GRNController();
                controller.Approve(grnId);

                MessageBox.Show(
                    $"GRN {grnNumber} Approved!\nInventory has been updated.",
                    "Approved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadPendingGRNs(); //  Refresh — approved GRN disappears
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) return;

                var selectedRow = dataGridView1.SelectedRows[0];

                // Guard against empty rows
                if (selectedRow.Cells["GRNId"].Value == null) return;

                int grnId = Convert.ToInt32(selectedRow.Cells["GRNId"].Value);

                var controller = new GRNController();
                var lines = controller.GetLines(grnId).ToList();

                dataGridView2.DataSource = null;
                dataGridView2.DataSource = lines;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
