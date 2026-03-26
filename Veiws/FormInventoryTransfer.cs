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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace AfiErpSystem.Veiws
{
    public partial class FormInventoryTransfer : Form
    {

        private int _savedTransferId = 0;
        private List<InventoryTransferLine> _lineBuffer = new List<InventoryTransferLine>();



        public FormInventoryTransfer()
        {
            InitializeComponent();
        }

        private void FormInventoryTransfer_Load(object sender, EventArgs e)
        {

            LoadLocations();
            LoadItems();
            SetupGrid();

        }


        private void LoadLocations()
        {
            var controller = new LocationController();
            var locations = controller.GetAll().ToList();

            cmbFromLocation.DataSource = locations;
            cmbFromLocation.DisplayMember = "WarehouseName";
            cmbFromLocation.ValueMember = "LocationId";
            cmbFromLocation.SelectedIndex = -1;

            //  Clone list for To Location
            cmbToLocation.DataSource = controller.GetAll().ToList();
            cmbToLocation.DisplayMember = "WarehouseName";
            cmbToLocation.ValueMember = "LocationId";
            cmbToLocation.SelectedIndex = -1;
        }

        private void LoadItems()
        {
            var controller = new ItemController();
            var items = controller.GetAll().ToList();
            cmbItem.DataSource = items;
            cmbItem.DisplayMember = "ItemName";
            cmbItem.ValueMember = "ItemId";
            cmbItem.SelectedIndex = -1;
        }

        private void SetupGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "ItemCode", HeaderText = "Item Code", DataPropertyName = "ItemCode", Width = 100 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "ItemName", HeaderText = "Item Name", DataPropertyName = "ItemName", Width = 180 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "Quantity", HeaderText = "Quantity", DataPropertyName = "Quantity", Width = 80 });

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
        }

        private void RefreshGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _lineBuffer;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = cmbItem.SelectedItem as Item;

                if (selectedItem == null)
                    throw new Exception("Please select an Item.");
                if (nudQuantity.Value <= 0)
                    throw new Exception("Quantity must be greater than zero.");

                //  Check for duplicate item in buffer
                if (_lineBuffer.Any(x => x.ItemId == selectedItem.ItemId))
                    throw new Exception("Item already added. Remove it first to change quantity.");

                _lineBuffer.Add(new InventoryTransferLine
                {
                    ItemId = selectedItem.ItemId,
                    ItemCode = selectedItem.ItemCode,
                    ItemName = selectedItem.ItemName,
                    Quantity = nudQuantity.Value
                });

                RefreshGrid();
                ClearLineFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTransferNumber.Text))
                    throw new Exception("Transfer Number is required.");
                if (cmbFromLocation.SelectedValue == null)
                    throw new Exception("From Location is required.");
                if (cmbToLocation.SelectedValue == null)
                    throw new Exception("To Location is required.");
                if ((int)cmbFromLocation.SelectedValue == (int)cmbToLocation.SelectedValue)
                    throw new Exception("From and To locations cannot be the same.");
                if (_lineBuffer.Count == 0)
                    throw new Exception("Please add at least one item.");

                var transfer = new InventoryTransfer
                {
                    TransferNumber = txtTransferNumber.Text,
                    FromLocationId = (int)cmbFromLocation.SelectedValue,
                    ToLocationId = (int)cmbToLocation.SelectedValue,
                    TransferDate = dtpTransferDate.Value
                };

                var controller = new InventoryTransferController();
                _savedTransferId = controller.Save(transfer);

                // Save all lines
                foreach (var line in _lineBuffer)
                {
                    line.TransferId = _savedTransferId;
                    controller.AddLine(line);
                }

                MessageBox.Show(
                    $"Transfer saved!\nTransfer ID: {_savedTransferId}\nItems: {_lineBuffer.Count}\n\nClick APPROVE to move stock.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Lock fields after save
                txtTransferNumber.Enabled = false;
                cmbFromLocation.Enabled = false;
                cmbToLocation.Enabled = false;
                dtpTransferDate.Enabled = false;
                cmbItem.Enabled = false;
                nudQuantity.Enabled = false;
                btnAddItem.Enabled = false;
                btnRemoveLine.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (_savedTransferId <= 0)
                    throw new Exception("Please save the Transfer first.");

                var confirm = MessageBox.Show(
                    $"Approve Transfer: {txtTransferNumber.Text}?\nThis will move stock between locations.",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                var controller = new InventoryTransferController();
                controller.Approve(_savedTransferId);

                MessageBox.Show(
                    "Transfer Approved! Stock has been moved.",
                    "Approved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        private void ClearForm()
        {
            txtTransferNumber.Text = string.Empty;
            cmbFromLocation.SelectedIndex = -1;
            cmbToLocation.SelectedIndex = -1;
            dtpTransferDate.Value = DateTime.Now;
            _savedTransferId = 0;
            _lineBuffer.Clear();
            RefreshGrid();
            ClearLineFields();

            // Re-enable all fields
            txtTransferNumber.Enabled = true;
            cmbFromLocation.Enabled = true;
            cmbToLocation.Enabled = true;
            dtpTransferDate.Enabled = true;
            cmbItem.Enabled = true;
            nudQuantity.Enabled = true;
            btnAddItem.Enabled = true;
            btnRemoveLine.Enabled = true;
        }

        private void ClearLineFields()
        {
            cmbItem.SelectedIndex = -1;
            nudQuantity.Value = 0;
        }

        private void btnPendingTransfers_Click(object sender, EventArgs e)
        {
            using (var popup = new FormPendingTransfers())
            {
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.ShowDialog(this);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            int index = dataGridView1.SelectedRows[0].Index;
            if (index >= 0 && index < _lineBuffer.Count)
            {
                _lineBuffer.RemoveAt(index);
                RefreshGrid();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
