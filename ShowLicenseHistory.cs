using DriversBussinesLayer;
using LicenseBussinessLayer;
using LisenceHistoryBussinessLayer;
using LocalDLBussinessLayer;
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
    public partial class frmShowLicenseHistory : Form
    {
        private int App_ID;
        clsLocalDrivingLicenseApplication LDLA;
        clsLicense _Licesne = new clsLicense();
        DataTable dt = new DataTable();
        public frmShowLicenseHistory(int Application_ID)
        {
            InitializeComponent();
            App_ID = Application_ID;
            LDLA = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByApplicationID(Application_ID);
            if (LDLA==null)
            {
                _Licesne = clsLicense.FindLicenseByApplicationID(Application_ID);
            }
        }

        public frmShowLicenseHistory(clsDrivers Driver)
        {
            InitializeComponent();
            
                _Licesne = clsLicense.FindLicenseByDriverID(Driver.DriverID);
            
        }

        private void _RefreshLocalLicenseDgv()
        {
            if (LDLA != null)
            {
                dt = clsLicenseHistory.GetLicenseHistory(LDLA.ApplicantPersonID);
            }
            else
            {
                dt = clsLicenseHistory.GetLicenseHistory(_Licesne.driver.Person.PersonID);
            }
            if (dt.Rows.Count>0)
            {
                dgvLocalHistory.DataSource = dt;
                dgvLocalHistory.Columns[0].HeaderText = "Lic ID";
                dgvLocalHistory.Columns[1].HeaderText = "App ID";
                dgvLocalHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalHistory.Columns[5].HeaderText = "is Active";
            }
        }

        private void _RefreshInternationalLicenseDgv()
        {
            if (LDLA!=null)
            {
                dt = clsLicenseHistory.GetInternationalLicenseHistory(LDLA.ApplicantPersonID);
            }
            else
            {
                dt = clsLicenseHistory.GetInternationalLicenseHistory(_Licesne.driver.Person.PersonID);
            }
            
            if (dt.Rows.Count > 0)
            {
                dgvInternational.DataSource = dt;
                dgvInternational.Columns[0].HeaderText = "Int License ID";
                dgvInternational.Columns[1].HeaderText = "App ID";
                dgvInternational.Columns[2].HeaderText = "Local Lic ID";
                dgvInternational.Columns[3].HeaderText = "Issue Date";
                dgvInternational.Columns[4].HeaderText = "Expiration Date";
                dgvInternational.Columns[5].HeaderText = "is Active";
            }
        }

        private void frmShowLicenseHistory_Load(object sender, EventArgs e)
        {
            if (LDLA!=null)
            {
             personInfoCardWithFilter1.LoadPersonInfo(LDLA.ApplicantPersonID);
            personInfoCardWithFilter1.PersonID = LDLA.ApplicantPersonID;
            personInfoCardWithFilter1.ShowFilter = false;
            _RefreshLocalLicenseDgv();
            _RefreshInternationalLicenseDgv();   
            }
            else
            {
                personInfoCardWithFilter1.LoadPersonInfo(_Licesne.driver.Person.PersonID);
                personInfoCardWithFilter1.PersonID = _Licesne.driver.Person.PersonID;
                personInfoCardWithFilter1.ShowFilter = false;
                _RefreshLocalLicenseDgv();
                _RefreshInternationalLicenseDgv();
            }


        }
    }
}
