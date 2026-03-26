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
    public partial class FormGrn : Form
    {

        // STATE
        private List<GRNLine> _lineBuffer = new List<GRNLine>();
        private int _savedGRNId = 0; // GRN Id to Approve after saving


        public FormGrn()
        {
            InitializeComponent();
        }

        private void FormGrn_Load(object sender, EventArgs e)
        {

            LoadSuppliers();
            LoadBillLocations();
            LoadItems();
            LoadLocations();
            SetupGrid();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }



        // Loaders
        private void LoadSuppliers()
        {
            var controller = new BusinessPartnerController();
            var suppliers = controller.GetByType("Supplier").ToList();
            cmbSupplier.DataSource = suppliers;
            cmbSupplier.DisplayMember = "BPName";
            cmbSupplier.ValueMember = "BPId";
            cmbSupplier.SelectedIndex = -1;
        }

        private void LoadBillLocations()
        {
            var controller = new BillLocationController();
            var billLocations = controller.GetAll().ToList();
            cmbBillLocation.DataSource = billLocations;
            cmbBillLocation.DisplayMember = "Name";
            cmbBillLocation.ValueMember = "BillLocationId";
            cmbBillLocation.SelectedIndex = -1;
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

        private void LoadLocations()
        {
            var controller = new LocationController();
            var locations = controller.GetAll().ToList();
            cmbLocation.DataSource = locations;
            cmbLocation.DisplayMember = "WarehouseName";
            cmbLocation.ValueMember = "LocationId";
            cmbLocation.SelectedIndex = -1;
        }

        private void SetupGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            { HeaderText = "Item Code", DataPropertyName = "ItemCode", Width = 100 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            { HeaderText = "Item Name", DataPropertyName = "ItemName", Width = 150 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            { HeaderText = "Quantity", DataPropertyName = "Quantity", Width = 80 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            { HeaderText = "Warehouse", DataPropertyName = "WarehouseName", Width = 120 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            { HeaderText = "Rack", DataPropertyName = "Rack", Width = 60 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            { HeaderText = "Cell", DataPropertyName = "Cell", Width = 60 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            { HeaderText = "Stack", DataPropertyName = "Stack", Width = 60 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            { HeaderText = "Batch", DataPropertyName = "BatchId", Width = 60 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            { HeaderText = "Serial", DataPropertyName = "SerialId", Width = 60 });

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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtGRNNumber.Text))
                    throw new Exception("GRN Number is required.");
                if (cmbSupplier.SelectedValue == null)
                    throw new Exception("Supplier is required.");
                if (cmbBillLocation.SelectedValue == null)
                    throw new Exception("Bill Location is required.");
                if (_lineBuffer.Count == 0)
                    throw new Exception("Please add at least one GRN Line.");

                var grn = new GRN
                {
                    GRNNumber = txtGRNNumber.Text,
                    SupplierId = (int)cmbSupplier.SelectedValue,
                    GRNDate = dtpGRNDate.Value,
                    BillLocationId = (int)cmbBillLocation.SelectedValue,
                    Remark = txtRemark.Text
                };

                var controller = new GRNController();

                _savedGRNId = controller.Save(grn); //  Store the ID

                foreach (var line in _lineBuffer)
                {
                    line.GRNId = _savedGRNId;
                    controller.AddLine(line);
                }

                MessageBox.Show(
                    $"GRN saved successfully!\nGRN ID: {_savedGRNId}\nLines saved: {_lineBuffer.Count}\n\nClick APPROVE to update Inventory.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //  DO NOT clear form — user still needs to approve
                // Lock header fields so user can't edit after save
                txtGRNNumber.Enabled = false;
                cmbSupplier.Enabled = false;
                cmbBillLocation.Enabled = false;
                dtpGRNDate.Enabled = false;
                txtRemark.Enabled = false;

                //  Also lock Add Item / line fields — lines already saved
                cmbItem.Enabled = false;
                cmbLocation.Enabled = false;
                cmbBatch.Enabled = false;
                cmbSerial.Enabled = false;
                nudQuantity.Enabled = false;
                button1.Enabled = false; // Add Item button
                button2.Enabled = false; // Remove line button
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = cmbItem.SelectedItem as Item;
                var selectedLocation = cmbLocation.SelectedItem as Location;

                if (selectedItem == null)
                    throw new Exception("Please select an Item.");
                if (selectedLocation == null)
                    throw new Exception("Please select a Location.");
                if (nudQuantity.Value <= 0)
                    throw new Exception("Quantity must be greater than zero.");

                int? batchId = null;
                int? serialId = null;
                if (cmbBatch.SelectedValue is int bid) batchId = bid;
                if (cmbSerial.SelectedValue is int sid) serialId = sid;

                //  Just add to local buffer — nothing saved to DB yet
                _lineBuffer.Add(new GRNLine
                {
                    ItemId = selectedItem.ItemId,
                    ItemCode = selectedItem.ItemCode,
                    ItemName = selectedItem.ItemName,
                    Quantity = nudQuantity.Value,
                    LocationId = selectedLocation.LocationId,
                    WarehouseName = selectedLocation.WarehouseName,
                    Rack = selectedLocation.Rack,
                    Cell = selectedLocation.Cell,
                    Stack = selectedLocation.Stack,
                    BatchId = batchId,
                    SerialId = serialId
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (_savedGRNId <= 0)
                    throw new Exception("Please save the GRN first before approving.");

                var confirm = MessageBox.Show(
                    $"Approve GRN: {txtGRNNumber.Text}?\nThis will update Inventory.",
                    "Confirm Approve",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                var controller = new GRNController();
                controller.Approve(_savedGRNId);

                MessageBox.Show("GRN Approved! Inventory has been updated.",
                    "Approved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm(); //  Now safe to clear after approval
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClearForm();
        }


        private void ClearForm()
        {
            txtGRNNumber.Clear();
            txtRemark.Clear();
            cmbSupplier.SelectedIndex = -1;
            cmbBillLocation.SelectedIndex = -1;
            dtpGRNDate.Value = DateTime.Now;
            _lineBuffer.Clear();
            _savedGRNId = 0; //  Reset saved ID
            RefreshGrid();
            ClearLineFields();

            //  Re-enable all fields for next GRN
            txtGRNNumber.Enabled = true;
            cmbSupplier.Enabled = true;
            cmbBillLocation.Enabled = true;
            dtpGRNDate.Enabled = true;
            txtRemark.Enabled = true;
            cmbItem.Enabled = true;
            cmbLocation.Enabled = true;
            cmbBatch.Enabled = true;
            cmbSerial.Enabled = true;
            nudQuantity.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void ClearLineFields()
        {
            cmbItem.SelectedIndex = -1;
            cmbLocation.SelectedIndex = -1;
            cmbBatch.SelectedIndex = -1;
            cmbSerial.SelectedIndex = -1;
            nudQuantity.Value = 0;
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

        private void button7_Click(object sender, EventArgs e)
        {
            //  Opens as a popup dialog
            using (var popup = new FormPendingGRNs())
            {
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.ShowDialog(this);
            }
        }
    }
}
