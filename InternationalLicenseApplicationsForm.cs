using InternationalLicensesBussiness;
using LocalDLBussinessLayer;
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
    public partial class frmInternationalLicenseApplicationsForm : Form
    {
        private DataTable _dtAllInternationalLicenses;
        public frmInternationalLicenseApplicationsForm()
        {
            InitializeComponent();
        }
        private void _RefreshDgv()
        {
            _dtAllInternationalLicenses = clsInternational.GetListInternationalLicenses();
            

            dgvInternationalLicencesApplications.DataSource = _dtAllInternationalLicenses;

            if (dgvInternationalLicencesApplications.Rows.Count > 0)
            {
                dgvInternationalLicencesApplications.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicencesApplications.Columns[0].Width = 70;

                dgvInternationalLicencesApplications.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicencesApplications.Columns[1].Width = 70;

                dgvInternationalLicencesApplications.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicencesApplications.Columns[2].Width = 70;

                dgvInternationalLicencesApplications.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicencesApplications.Columns[3].Width = 70;

                dgvInternationalLicencesApplications.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicencesApplications.Columns[4].Width = 230;

                dgvInternationalLicencesApplications.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicencesApplications.Columns[5].Width = 180;

                dgvInternationalLicencesApplications.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicencesApplications.Columns[6].Width = 120;

            }
        }
        private void _FillComboBoxes()
        {
           
            cbIsAtive.Visible = false;
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("International License ID");
            cbFilter.Items.Add("Application ID");
            cbFilter.Items.Add("Driver ID");
            cbFilter.Items.Add("Local License ID");
            cbFilter.Items.Add("Is Active");
            cbFilter.SelectedIndex = 0;
            cbIsAtive.Items.Add("Yes");
            cbIsAtive.Items.Add("No");
            cbIsAtive.SelectedIndex = 0;


        }
        private void InternationalLicenseApplicationsForm_Load(object sender, EventArgs e)
        {
            _FillComboBoxes();
            _RefreshDgv();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cbFilter.SelectedIndex != 0);
            if (cbFilter.SelectedIndex == cbFilter.FindString("Is Active"))
            {
                cbIsAtive.Visible = true;
                txtFilter.Visible = false;
            }
            else
            {
                cbIsAtive.Visible = false;
                
            }

            if (cbFilter.SelectedIndex == 0 || string.IsNullOrEmpty(txtFilter.Text))
            {
                _RefreshDgv();
                txtFilter.Clear();
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {

            if (!int.TryParse(txtFilter.Text, out int intValue)&& !string.IsNullOrEmpty(txtFilter.Text))
            {
                txtFilter.Clear();
                return;
            }

            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    {
                        FilterColumn = "ApplicationID";
                        break;
                    };

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }

            


            if (txtFilter.Text == "" || cbFilter.SelectedIndex == 0)
            {
                _RefreshDgv();
                return;
            }



            _dtAllInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());

            
        }

        private void cbIsAtive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIsAtive.SelectedIndex == cbIsAtive.FindString("Yes"))
            {
                _dtAllInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsActive", 1);
            }
            else
            {
                _dtAllInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsActive", 1);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmIssueInternationalLicense frm = new frmIssueInternationalLicense();
            frm.ShowDialog();
            _RefreshDgv();
        }

    }
}
