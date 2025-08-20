using ApplicationTypesBussinessLayer;
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
    public partial class EditApplicationType : Form
    {
        private clsApplicationTypes _AppType;
        private int _AppTypeId;
        public EditApplicationType(int AppTypeID)
        {
            InitializeComponent();
            _AppTypeId = AppTypeID;
        }

        private void EditApplicationType_Load(object sender, EventArgs e)
        {
            _AppType = clsApplicationTypes.FindApplicationTypeByID(_AppTypeId);
            if (_AppType!=null)
            {
                lbAppTypeID.Text = _AppType.ApplicationTypeID.ToString();
                txtNewFees.Text = _AppType.ApplicationFees.ToString();
                txtNewTitle.Text = _AppType.ApplicationTypeTitle.ToString();  
            }
           
        }

        private bool AreInputsIncorrect()
        {
            return (string.IsNullOrEmpty(txtNewTitle.Text.Trim())||string.IsNullOrEmpty(txtNewFees.Text.Trim())|| txtNewTitle.Text.Trim() == _AppType.ApplicationTypeTitle);
        }
        private void button1_Click(object sender, EventArgs e) // Save Button
        {
            if (AreInputsIncorrect())
            {
                MessageBox.Show("Inputs Invalid");
                return;
            }
            _AppType.ApplicationTypeTitle = txtNewTitle.Text.Trim();
            _AppType.ApplicationFees = Convert.ToDouble(txtNewFees.Text.Trim());
            if (_AppType.UpdateApplicationType())
            {
                MessageBox.Show("Updated Successfully");

            }
            else
            {
                MessageBox.Show("Updating Failed");

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
