using LicenseBussinessLayer;
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
    public partial class ctrlDriversLicenseInfoWithFilter : UserControl
    {

        public event Action<int> OnLicenseFound;

        protected virtual void FindLicense(int LicenseID)
        {
            Action<int> Handler = OnLicenseFound;
            if (Handler!=null)
            {
                Handler(LicenseID);
            }
        }

        private int _LicenseID;
        public int LicenseID
        {
            get { return driversInfo1.LicenseID; }

            set { driversInfo1.LicenseID = value; }
        }

        public clsLicense SelectedLicense
        {
            get
            { return driversInfo1.SelectedLicense; }
        }

        private bool _FilterEnabled;
        public bool FilterEnabled
        { 
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        public void LoadLicenseInfos(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();
            driversInfo1.LoadDriversCardInfoByLicenseID(LicenseID);
            _LicenseID = driversInfo1.LicenseID;
            if (OnLicenseFound !=null && FilterEnabled)
            {
                OnLicenseFound(_LicenseID);
            }

        }


        public ctrlDriversLicenseInfoWithFilter()
        {
            InitializeComponent();
        }




        private void ctrlDriversLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;
            }
            _LicenseID = int.Parse(txtLicenseID.Text);
            LoadLicenseInfos(_LicenseID);

        }



        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtLicenseID, null);
            }

        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }
        }
    }
}
