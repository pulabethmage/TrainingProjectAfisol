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
    public partial class FormPendingTransfers : Form
    {
        public FormPendingTransfers()
        {
            InitializeComponent();
        }

        private void FormPendingTransfers_Load(object sender, EventArgs e)
        {
            LoadPendingTransfers();
            SetupLinesGrid();
        }

        private void LoadPendingTransfers()
        {
            try
            {
                var controller = new InventoryTransferController();
                var pending = controller.GetPending().ToList();

                dataGridView1.DataSource = null;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "TransferId", HeaderText = "ID", DataPropertyName = "TransferId", Width = 50 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "TransferNumber", HeaderText = "Transfer No", DataPropertyName = "TransferNumber", Width = 120 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "FromLocation", HeaderText = "From", DataPropertyName = "FromLocation", Width = 150 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "ToLocation", HeaderText = "To", DataPropertyName = "ToLocation", Width = 150 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "TransferDate", HeaderText = "Date", DataPropertyName = "TransferDate", Width = 120 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "Status", HeaderText = "Status", DataPropertyName = "Status", Width = 80 });

                dataGridView1.DataSource = pending;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.RowHeadersVisible = false;

                this.Text = $"Pending Transfers ({pending.Count})";
                dataGridView2.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupLinesGrid()
        {
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.Columns.Clear();

            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "ItemCode", HeaderText = "Item Code", DataPropertyName = "ItemCode", Width = 100 });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "ItemName", HeaderText = "Item Name", DataPropertyName = "ItemName", Width = 200 });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "Quantity", HeaderText = "Quantity", DataPropertyName = "Quantity", Width = 80 });

            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.ReadOnly = true;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.RowHeadersVisible = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) return;
                var selectedRow = dataGridView1.SelectedRows[0];
                if (selectedRow.Cells["TransferId"].Value == null) return;

                int transferId = Convert.ToInt32(selectedRow.Cells["TransferId"].Value);

                var controller = new InventoryTransferController();
                var lines = controller.GetLines(transferId).ToList();

                dataGridView2.DataSource = null;
                dataGridView2.DataSource = lines;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                    throw new Exception("Please select a Transfer to approve.");

                var selectedRow = dataGridView1.SelectedRows[0];
                int transferId = Convert.ToInt32(selectedRow.Cells["TransferId"].Value);
                string transferNum = selectedRow.Cells["TransferNumber"].Value.ToString();

                var confirm = MessageBox.Show(
                    $"Approve Transfer: {transferNum}?\nThis will move stock between locations.",
                    "Confirm Approve",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                var controller = new InventoryTransferController();
                controller.Approve(transferId);

                MessageBox.Show(
                    $"Transfer {transferNum} Approved!\nStock has been moved.",
                    "Approved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadPendingTransfers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
