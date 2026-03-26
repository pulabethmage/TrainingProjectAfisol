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
using System.Xml.Linq;

namespace AfiErpSystem.Veiws
{
    public partial class FormBillLocations : Form
    {
        public FormBillLocations()
        {
            InitializeComponent();
        }

        private void FormBillLocations_Load(object sender, EventArgs e)
        {
            LoadBillLocations();

        }

        private void saveBtnpartner_Click(object sender, EventArgs e)
        {
            try
            {
                BillLocation billLocation = new BillLocation
                {
                    Name = txtName.Text,
                    Address = txtAddress.Text
                };

                BillLocationController controller = new BillLocationController();
                int newId = controller.Save(billLocation);

                MessageBox.Show(
                    $"Bill Location saved successfully!\nBill Location ID: {newId}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                ClearForm();
                LoadBillLocations(); // Refresh grid
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
            txtName.Clear();
            txtAddress.Clear();
        }

        private void LoadBillLocations()
        {
            try
            {
                BillLocationController controller = new BillLocationController();
                dataGridView1.DataSource = controller.GetAll().ToList();

                // Hide all columns first
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

                ShowColumn("BillLocationId", "ID", 50);
                ShowColumn("Name", "Name", 150);
                ShowColumn("Address", "Address", 250);

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

        
    }
}
