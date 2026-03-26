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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace AfiErpSystem.Veiws
{
    public partial class FormLocations : Form
    {
        public FormLocations()
        {
            InitializeComponent();
        }

        private void FormLocations_Load(object sender, EventArgs e)
        {

            LoadLocations();
            LoadComboBoxes();
        }

        // Use Shown event if grid is inside a Panel
        private void FormLocations_Shown(object sender, EventArgs e)
        {
            
        }

        private void saveBtnpartner_Click(object sender, EventArgs e)
        {
            try
            {
                Location location = new Location
                {
                    WarehouseName = txtWarehouse.Text,
                    Rack = cmbRack.SelectedItem?.ToString(),   // ✅ combobox
                    Cell = cmbCell.SelectedItem?.ToString(),   // ✅ combobox
                    Stack = cmbStack.SelectedItem?.ToString()   // ✅ combobox
                };

                LocationController controller = new LocationController();
                int newId = controller.Save(location);

                MessageBox.Show(
                    $"Location saved successfully!\nLocation ID: {newId}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                ClearForm();
                LoadLocations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtWarehouse.Clear();
            cmbRack.SelectedIndex = -1;  
            cmbCell.SelectedIndex = -1;
            cmbStack.SelectedIndex = -1;
        }


        private void LoadLocations()
        {
            try
            {
                LocationController controller = new LocationController();
                dataGridView1.DataSource = controller.GetAll().ToList();

                //Hide all first
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                    col.Visible = false;

                // Show only needed columns
                void ShowColumn(string col, string header, int width = 0)
                {
                    if (dataGridView1.Columns.Contains(col))
                    {
                        dataGridView1.Columns[col].Visible = true;
                        dataGridView1.Columns[col].HeaderText = header;
                        if (width > 0)
                            dataGridView1.Columns[col].Width = width;
                    }
                }

                ShowColumn("LocationId", "ID", 50);
                ShowColumn("WarehouseName", "Warehouse", 150);
                ShowColumn("Rack", "Rack", 80);
                ShowColumn("Cell", "Cell", 80);
                ShowColumn("Stack", "Stack", 80);

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

       

        private void LoadComboBoxes()
        {
            // Rack — A to Z
            cmbRack.Items.Clear();
            for (char c = 'A'; c <= 'Z'; c++)
                cmbRack.Items.Add(c.ToString());

            // Cell — 01 to 20
            cmbCell.Items.Clear();
            for (int i = 1; i <= 20; i++)
                cmbCell.Items.Add(i.ToString("D2")); // D2 = 01, 02, 03...

            // Stack — Top, Middle, Bottom (or levels 1-10)
            cmbStack.Items.Clear();
            cmbStack.Items.AddRange(new string[]
            {
                "Top",
                "Middle",
                "Bottom"
            });

            // No default selection
            cmbRack.SelectedIndex = -1;
            cmbCell.SelectedIndex = -1;
            cmbStack.SelectedIndex = -1;
        }


    }
}
