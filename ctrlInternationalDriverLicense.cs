using Driver_Licence_Project.Properties;
using InternationalLicensesBussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_Licence_Project
{
    public partial class ctrlInternationalDriverLicense : UserControl
    {
        public ctrlInternationalDriverLicense()
        {
            InitializeComponent();
        }
        clsInternational International;
        private void HandelPersonalImage()
        {
            if (File.Exists(International.drivers.Person.ImagePath))
            {
                try
                {
                    pbPersonal.Image = Image.FromFile(International.drivers.Person.ImagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Image file not found at the specified location.");
            }
        }
        public void LoadInfos(int InternationalLicenseID)
        {
             International = clsInternational.FindLicenseByID(InternationalLicenseID);
            if (International!=null)
            {
                lblApplicationID.Text = International.ApplicationID.ToString();
                lblDateOfBirth.Text = International.drivers.Person.DateOfBirth.ToShortDateString();
                lblDriverID.Text = International.DriverID.ToString();
                lblExpirationDate.Text = International.ExpirationDate.ToShortDateString();
                lblFullName.Text = International.drivers.Person.FullName;
                if (International.drivers.Person.Gendor==0)
                {
                    lblGendor.Text = "Male";
                    pbPersonal.Image = Resources.MaleUser;
                }
                else
                {
                    lblGendor.Text = "Female";
                    pbPersonal.Image = Resources.FemalUser;

                }
                lblInternationalLicenseID.Text = International.InternationalLicenseID.ToString();
                if (International.IsActive == 0)
                {
                    lblIsActive.Text = "No";
                }
                else
                {
                    lblIsActive.Text = "Yes";
                }
                lblLocalLicenseID.Text = International.IssuedUsingLocalLicenseID.ToString();
                lblNationalNo.Text = International.drivers.Person.NationalNo.ToString();
                lblIssueDate.Text = International.IssueDate.ToShortDateString();
                HandelPersonalImage();


            }


        }
        private void ctrlInternationalDriverLicense_Load(object sender, EventArgs e)
        {

        }
    }
}
