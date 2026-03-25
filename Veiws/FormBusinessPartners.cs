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
    public partial class FormBusinessPartners : Form
    {
        public FormBusinessPartners()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void saveBtnpartner_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessPartner bp = new BusinessPartner
                {
                    BPName = txtBPName.Text,
                    BPType = cmbBPType.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text,
                    Address = txtAddress.Text
                };

                BusinessPartnerController controller = new BusinessPartnerController();
                int newId = controller.Save(bp);

                // Success message showing the new ID
                MessageBox.Show(
                    $"Business Partner saved successfully!\nBP ID: {newId}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                ClearForm(); // optional - clear fields after save
                LoadBusinessPartners();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }


        }

        // Optional: clear all fields after successful save
        private void ClearForm()
        {
            txtBPName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            cmbBPType.SelectedIndex = -1;
        }

        private void FormBusinessPartners_Load(object sender, EventArgs e)
        {
            LoadBusinessPartners();
        }

        private void LoadBusinessPartners()
        {
            try
            {
                BusinessPartnerController controller = new BusinessPartnerController();

                dataGridView1.DataSource = controller.GetAll().ToList();

                // Format column headers
                dataGridView1.Columns["BPId"].HeaderText = "ID";
                dataGridView1.Columns["BPName"].HeaderText = "Name";
                dataGridView1.Columns["BPType"].HeaderText = "Type";
                dataGridView1.Columns["Phone"].HeaderText = "Phone";
                dataGridView1.Columns["Email"].HeaderText = "Email";
                dataGridView1.Columns["Address"].HeaderText = "Address";
                dataGridView1.Columns["CreatedDate"].HeaderText = "Created Date";

                // Optional: improve appearance
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
