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
    public partial class frmShowInternationalLicenseDetails : Form
    {
        private int _InternationalLicenseID;
        public frmShowInternationalLicenseDetails(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = InternationalLicenseID;
        }

        private void frmShowInternationalLicenseDetails_Load(object sender, EventArgs e)
        {
            ctrlInternationalDriverLicense1.LoadInfos(_InternationalLicenseID);
        }
    }
}
