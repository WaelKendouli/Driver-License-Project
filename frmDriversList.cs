using ApplicationsBussinessLayer;
using DetainedLicensesBussiness;
using DriversBussinesLayer;
using PeopleBussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_Licence_Project
{
    public partial class frmDriversList : Form
    {
        public frmDriversList()
        {
            InitializeComponent();
        }
        DataTable _dt = new DataTable();
        private void _RefreshGrid()
        {
            _dt = clsDrivers.GetListOfDrivers();
            if (_dt.Rows.Count > 0)
            {
                dgvListDrivers.DataSource = _dt;
                dgvListDrivers.Columns[0].HeaderText = "Driver ID";
                dgvListDrivers.Columns[0].Width = 50;
                
                dgvListDrivers.Columns[1].HeaderText = "Person ID";
                dgvListDrivers.Columns[1].Width = 50;
                
                dgvListDrivers.Columns[2].HeaderText = "National No";
                dgvListDrivers.Columns[2].Width = 50;
                
                dgvListDrivers.Columns[3].HeaderText = "Full Name";
                dgvListDrivers.Columns[3].Width = 250;
                
                dgvListDrivers.Columns[4].HeaderText = "Date";
                dgvListDrivers.Columns[4].Width = 250;

                dgvListDrivers.Columns[5].HeaderText = "Active License";
                dgvListDrivers.Columns[5].Width = 50;



            }
        }
        private void frmDriversList_Load(object sender, EventArgs e)
        {
            _RefreshGrid();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonDetails frm = new frmShowPersonDetails((int)dgvListDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void showPersonHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsDrivers Driver = clsDrivers.FindDriver((int)dgvListDrivers.CurrentRow.Cells[0].Value);
           
            frmShowLicenseHistory frm = new frmShowLicenseHistory(Driver);
            frm.ShowDialog();
        }
    }
}
