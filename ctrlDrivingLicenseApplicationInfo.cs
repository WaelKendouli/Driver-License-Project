using LicenseBussinessLayer;
using LocalDLBussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_Licence_Project
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private int _LocalDrivingLicenseApplicationID = -1;

        public int LocalDrivingLicenseApplicationID 
        { get { return _LocalDrivingLicenseApplicationID; } }
        

        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }
        private clsLocalDrivingLicenseApplication DL_Application = new clsLocalDrivingLicenseApplication();

        public void LoadLocalDrivingLicenseInfoByApplicationID(int LDL_ApplicationID)
        {
            llShowLicenseInfo.Enabled = clsLicense.isLicenseExist(LDL_ApplicationID);
            DL_Application = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByApplicationID(LDL_ApplicationID);
            if (DL_Application!=null)
            {
                lbDLAppID.Text = DL_Application.LocalLicenseApplicationID.ToString();
                _LocalDrivingLicenseApplicationID = DL_Application.LocalLicenseApplicationID;
                lbAppID.Text = DL_Application.ApplicationID.ToString();
                lbAppliedForLicense.Text = DL_Application._LicenseClass.ClassName.ToString();
                if (DL_Application.ApplicationStatusNum == 1)
                {
                    lbAppStatus.Text = "New";
                }
                else if (DL_Application.ApplicationStatusNum == 2)
                {
                    lbAppStatus.Text = "Canceled";

                }
                else
                {
                    lbAppStatus.Text = "Completed";
                }
                lbAppType.Text = DL_Application.AppType.ApplicationTypeTitle;
                lbCreatedBy.Text = DL_Application.User.UserName;
                lbDate.Text = DL_Application.ApplicationDate.ToShortDateString();
                lbFees.Text = DL_Application.PaidFees.ToString();
                lbStatusDate.Text = DL_Application.LastStatusDate.ToShortDateString();
                lbApplicant.Text = DL_Application.Person.FirstName + " " + DL_Application.Person.SecondName + " " + DL_Application.Person.ThirdName + " " + DL_Application.Person.LastName;

            }
        }
        public void LoadLocalDrivingLicenseInfo(int LDLA_ID)
        {
            
            DL_Application = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(LDLA_ID);
            if (DL_Application!=null)
            {
                llShowLicenseInfo.Enabled = clsLicense.isLicenseExist(DL_Application.ApplicationID);
                lbDLAppID.Text = DL_Application.LocalLicenseApplicationID.ToString();
                _LocalDrivingLicenseApplicationID = DL_Application.LocalLicenseApplicationID;
                lbAppID.Text = DL_Application.ApplicationID.ToString();
                lbAppliedForLicense.Text = DL_Application._LicenseClass.ClassName.ToString();
                if (DL_Application.ApplicationStatusNum == 1)
                {
                    lbAppStatus.Text = "New";
                }
                else if (DL_Application.ApplicationStatusNum == 2)
                {
                    lbAppStatus.Text = "Canceled";

                }
                else
                {
                    lbAppStatus.Text = "Completed";
                }
                lbAppType.Text = DL_Application.AppType.ApplicationTypeTitle;
                lbCreatedBy.Text = DL_Application.User.UserName;
                lbDate.Text = DL_Application.ApplicationDate.ToShortDateString();
                lbFees.Text = DL_Application.PaidFees.ToString();
                lbStatusDate.Text = DL_Application.LastStatusDate.ToShortDateString();
                lbApplicant.Text = DL_Application.Person.FirstName + " " + DL_Application.Person.SecondName + " " + DL_Application.Person.ThirdName + " " + DL_Application.Person.LastName;
            }

        }

        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            

        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonDetails frm = new frmShowPersonDetails(DL_Application.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverCard frm = new frmDriverCard(DL_Application.ApplicationID);
            frm.ShowDialog();
        }
    }
}
