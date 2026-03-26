using AfiErpSystem.Veiws;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AfiErpSystem
{
    public partial class Dashboard : Form
    {
        FormDashboard dashboardForm;
        AddItemForm addItemFrom;
        FormBusinessPartners businessPartnersForm;
        FormGrn grnForm;
        FormInventoryTransfer inventoryTransferForm;
        FormDispatch dispatchForm;
        FormLocations locationsForm;
        FormBillLocations billLocationsForm;
        FormInventory invetoryForm;


        public Dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(addItemFrom == null)
            {
                addItemFrom = new AddItemForm();
                addItemFrom.FormClosed += AddItemFrom_FormClosed;
                addItemFrom.MdiParent = this;
                addItemFrom.Dock = DockStyle.Fill;
                addItemFrom.Show();
            }
            else
            {
                addItemFrom.Activate();
            }
        }

        private void AddItemFrom_FormClosed(object sender, FormClosedEventArgs e)
        {
            addItemFrom = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(businessPartnersForm == null)
            {
                businessPartnersForm = new FormBusinessPartners();
                businessPartnersForm.FormClosed += BusinessPartnersForm_FormClosed;
                businessPartnersForm.MdiParent = this;
                businessPartnersForm.Dock = DockStyle.Fill;
                businessPartnersForm.Show();
            }
            else
            {
                businessPartnersForm.Activate();
            }
        }

        private void BusinessPartnersForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            businessPartnersForm = null;
        }

        bool sidebarExpand = true;

        private void sidebarTransition_Tick(object sender, EventArgs e)
        {

            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= 72)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= 247)
                {
                    sidebarExpand = true;
                    sidebarTransition.Stop();
                }

            }
        }

        private void hammenubtn_Click(object sender, EventArgs e)
        {

                sidebarTransition.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(dashboardForm == null)
            {
                dashboardForm = new FormDashboard();
                dashboardForm.FormClosed += Dashboard_FormClosed;
                dashboardForm.MdiParent = this;
                dashboardForm.Dock = DockStyle.Fill;
                dashboardForm.Show();
            }
            else
            {
                dashboardForm.Activate();
            }
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            dashboardForm = null;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            dashboardForm = new FormDashboard();
            dashboardForm.MdiParent = this;
            dashboardForm.Dock = DockStyle.Fill;
            dashboardForm.Show();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(grnForm == null)
            {
                grnForm = new FormGrn();
                grnForm.FormClosed += GrnForm_FormClosed;
                grnForm.MdiParent = this;
                grnForm.Dock = DockStyle.Fill;
                grnForm.Show();
            }
            else
            {
                grnForm.Activate();
            }
        }

        private void GrnForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            grnForm = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(inventoryTransferForm == null)
            {
                inventoryTransferForm = new FormInventoryTransfer();
                inventoryTransferForm.FormClosed += InventoryTransferForm_FormClosed;
                inventoryTransferForm.MdiParent = this;
                inventoryTransferForm.Dock = DockStyle.Fill;
                inventoryTransferForm.Show();
            }
            else
            {
                inventoryTransferForm.Activate();
            }
        }

        private void InventoryTransferForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           inventoryTransferForm = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(dispatchForm == null)
            {
                dispatchForm = new FormDispatch();
                dispatchForm.FormClosed += DispatchForm_FormClosed;
                dispatchForm.MdiParent = this;
                dispatchForm.Dock = DockStyle.Fill;
                dispatchForm.Show();
            }
            else
            {
                dispatchForm.Activate();
            }
        }

        private void DispatchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            dispatchForm = null;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(locationsForm == null)
            {
                locationsForm = new FormLocations();
                locationsForm.FormClosed += LocationsForm_FormClosed;
                locationsForm.MdiParent = this;
                locationsForm.Dock = DockStyle.Fill;
                locationsForm.Show();
            }
            else
            {
                locationsForm.Activate();
            }
        }

        private void LocationsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            locationsForm = null;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(billLocationsForm == null)
            {
                billLocationsForm = new FormBillLocations();
                billLocationsForm.FormClosed += BillLocationsForm_FormClosed;
                billLocationsForm.MdiParent = this;
                billLocationsForm.Dock = DockStyle.Fill;
                billLocationsForm.Show();
            }
            else
            {
                billLocationsForm.Activate();
            }
        }

        private void BillLocationsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           billLocationsForm = null;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if(invetoryForm == null)
            {
                invetoryForm = new FormInventory();
                invetoryForm.FormClosed += InvetoryForm_FormClosed;
                invetoryForm.MdiParent = this;
                invetoryForm.Dock = DockStyle.Fill;
                invetoryForm.Show();
            }
            else
            {
                invetoryForm.Activate();
            }
        }

        private void InvetoryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            invetoryForm = null;
        }
    }
}
