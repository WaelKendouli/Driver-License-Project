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
using LoggedInUser;
using UsersBussinessLayer;
namespace Driver_Licence_Project
{
    public partial class frmMainDashboard : Form
    {
        public frmMainDashboard()
        {
            InitializeComponent();
        }
        private int LoggedInPersonID;

        private void GetLoggedinPersonID(object sender, int ID)
        {
            LoggedInPersonID = ID;
            clsCurrentUser.GlobalUser = clsUser.FindUserByPersonID(ID);//Logged in user
        }

        
        private void frmMainDashboard_Load(object sender, EventArgs e)
        {

            

            // First show the login form
            using (LoginScreen loginForm = new LoginScreen())
            {
                loginForm.DataHandler += GetLoggedinPersonID;
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // If login successful, run the main form
                    this.Show();
                }
                else
                {
                    loginForm.Close();
                    this.Close();
                }
            }
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPeopleMangement frm = new frmPeopleMangement();
            frm.MdiParent = this;
            frm.Show();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Hide();

           
            LoginScreen loginForm = new LoginScreen();
            loginForm.DataHandler += GetLoggedinPersonID;
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
               
                this.Show(); 
            }
            else
            {
                
                this.Close();
            }

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersManagementForm frm = new UsersManagementForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void currentUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoginInfo frm = new frmLoginInfo(LoggedInPersonID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(LoggedInPersonID);
            frm.ShowDialog();
        }

        private void applicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmApplicationTypes frm = new frmApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frm = new frmManageTestTypes();
            frm.ShowDialog();
        }

        
        private void localLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewLocalDrivingLicence frm = new frmAddNewLocalDrivingLicence();
            frm.ShowDialog();
        }

        private void localDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicencesApplications frm = new frmLocalDrivingLicencesApplications();
            frm.ShowDialog();
        }

        private void internationalLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueInternationalLicense frm = new frmIssueInternationalLicense();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseApplicationsForm frm = new frmInternationalLicenseApplicationsForm();
            frm.ShowDialog();
        }

        private void renewDrivingLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewDrivingLicense frm = new frmRenewDrivingLicense();
            frm.ShowDialog();
        }

        private void reolacementForLostOrDamagedLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceForLostOrDamagedLicense frm = new frmReplaceForLostOrDamagedLicense();
            frm.ShowDialog();
        }

        private void manageDetainLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDetainedLicenses frm = new frmManageDetainedLicenses();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseDetainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDriversList frm = new frmDriversList();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicencesApplications frm = new frmLocalDrivingLicencesApplications();
            frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }
    }
}
