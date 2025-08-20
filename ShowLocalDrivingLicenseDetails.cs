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
    public partial class frmShowLocalDrivingLicenseDetails : Form
    {
        private int _DLA_ID;
        public frmShowLocalDrivingLicenseDetails(int DrivingLicenseApplicationApplicationID)
        {
            InitializeComponent();
            _DLA_ID = DrivingLicenseApplicationApplicationID;
        }
        
        private void frmShowLocalDrivingLicenseDetails_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseInfoByApplicationID(_DLA_ID);
        }
    }
}
