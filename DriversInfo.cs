using DetainedLicensesBussiness;
using Driver_Licence_Project.Properties;
using LicenseBussinessLayer;
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
    public partial class DriversInfo : UserControl
    {
        clsLicense _License;
        private int _ApplicationID;
        public int ApplicationID
        {
            get
            {
                return _ApplicationID;
            }
            set
            {
                _ApplicationID = value;
            }
        }

        private int _LicenseID;

        public int LicenseID 
            {
            get
            {
                return _LicenseID;
            }
            set
            {
                _LicenseID = value;
            } 
        }
        public clsLicense SelectedLicense
        {
            get 
            {
                return _License;
            }
        }

        public DriversInfo(int LDLA_ApplicationID)
        {
            InitializeComponent();
            _ApplicationID = LDLA_ApplicationID;
            
        }
        public DriversInfo()
        {
            InitializeComponent();
            

        }

        public void LoadDriversCardInfo(int LDLA_ApplicationID)
        {
            _License = clsLicense.FindLicenseByApplicationID(LDLA_ApplicationID);
            _ApplicationID = LDLA_ApplicationID;
            if (_License!=null)
            {
                lbClass.Text = _License._LicenseClass.ClassName;
                lbDriverID.Text = _License.DriverID.ToString();
                lbLicenseID.Text = _License.LicenseID.ToString();
                lbName.Text = _License.driver.Person.FirstName + " " + _License.driver.Person.SecondName + " " + _License.driver.Person.ThirdName + " " + _License.driver.Person.LastName;
                lbExperationDate.Text = _License.ExpirationDate.ToShortDateString();
                if (_License.driver.Person.Gendor == 0)
                {
                    lbGendor.Text = "Male";
                    pbPersonalPic.Image = Resources.MaleUser;
                }
                else
                {
                    lbGendor.Text = "Female";
                    pbPersonalPic.Image = Resources.FemalUser;

                }
                if (_License.IsActive == 0)
                {
                    lbIsActive.Text = "No";
                }
                else
                {
                    lbIsActive.Text = "Yes";
                }
                lbNotes.Text = _License.Notes;
                lbIssueDate.Text = _License.IssueDate.ToShortDateString();
                lbNationalNo.Text = _License.driver.Person.NationalNo;
                lbDateOfBirth.Text = _License.driver.Person.DateOfBirth.ToShortDateString();
                lbIssueReason.Text = _License.IssueReasonText;
                if (_License.driver.Person.ImagePath!=""&& File.Exists(_License.driver.Person.ImagePath))
                {
                    
                        pbPersonalPic.Image = Image.FromFile(_License.driver.Person.ImagePath);

                }
                else
                {
                    MessageBox.Show("Picture not found", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            if (clsDetainedLicenses.isLicenseDetained(_License.LicenseID))
            {
                lbIsDetained.Text = "Yes";
            }
            else
            {
                lbIsDetained.Text = "No";
            }
        }
        public void LoadDriversCardInfoByLicenseID(int LicenseID)
        {
            _License = clsLicense.FindLicenseByID(LicenseID);
            _LicenseID = LicenseID;
            if (_License != null)
            {
                lbClass.Text = _License._LicenseClass.ClassName;
                lbDriverID.Text = _License.DriverID.ToString();
                lbLicenseID.Text = _License.LicenseID.ToString();
                lbName.Text = _License.driver.Person.FirstName + " " + _License.driver.Person.SecondName + " " + _License.driver.Person.ThirdName + " " + _License.driver.Person.LastName;
                lbExperationDate.Text = _License.ExpirationDate.ToShortDateString();
                if (_License.driver.Person.Gendor == 0)
                {
                    lbGendor.Text = "Male";
                    pbPersonalPic.Image = Resources.MaleUser;
                }
                else
                {
                    lbGendor.Text = "Female";
                    pbPersonalPic.Image = Resources.FemalUser;

                }
                if (_License.IsActive == 0)
                {
                    lbIsActive.Text = "No";
                }
                else
                {
                    lbIsActive.Text = "Yes";
                }
                lbNotes.Text = _License.Notes;
                lbIssueDate.Text = _License.IssueDate.ToShortDateString();
                lbNationalNo.Text = _License.driver.Person.NationalNo;
                lbDateOfBirth.Text = _License.driver.Person.DateOfBirth.ToShortDateString();
                lbIssueReason.Text = _License.IssueReasonText;
                if (_License.driver.Person.ImagePath != "" && File.Exists(_License.driver.Person.ImagePath))
                {

                    pbPersonalPic.Image = Image.FromFile(_License.driver.Person.ImagePath);

                }
                else
                {
                    MessageBox.Show("Picture not found", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (clsDetainedLicenses.isLicenseDetained(_License.LicenseID))
                {
                    lbIsDetained.Text = "Yes";
                }
                else
                {
                    lbIsDetained.Text = "No";
                }
            }

        }

        private void ctrlDriversInfo_Load(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
