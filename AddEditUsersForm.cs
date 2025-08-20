using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsersBussinessLayer;

namespace Driver_Licence_Project
{
    public partial class frmAddEditUsers : Form
    {
        private int _PersonID;
        public frmAddEditUsers()
        {
            InitializeComponent();
            _PersonID = -1;
        }
        public frmAddEditUsers(int PersonId)
        {
            InitializeComponent();
            _PersonID = PersonId;
        }
        private clsUser _User = new clsUser();
        private void ucPersonInformationCard1_Load(object sender, EventArgs e)
        {
            
        }
       
        private void frmAddEditUsers_Load(object sender, EventArgs e)
        {
            personInfoCardWithFilter1.ResetInfosValues();
            if (_PersonID!=-1)
            {
                personInfoCardWithFilter1.PersonID = _PersonID;
                personInfoCardWithFilter1.LoadPersonInfo(_PersonID);
                personInfoCardWithFilter1.ShowFilter = false;
                tabControl1.SelectedIndex = 2;
                _User = clsUser.FindUserByPersonID(_PersonID);
                txtUsername.Text = _User.UserName;
                txtPassword.Text = _User.Password;
                txtConfirmPassword.Text = _User.Password;
                lbUserID.Text = _User.UserID.ToString();
                lbModeTitle.Text = "Update User";
                btnNext.Enabled = false;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (clsUser.IsUserExist(personInfoCardWithFilter1.PersonID)== false && personInfoCardWithFilter1.PersonID!=-1)
            {
                
                tabControl1.SelectedIndex++;
            }
            else
            {
                MessageBox.Show("This user is already in the database", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void personInfoCardWithFilter1_Load(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
           
        }
        private bool checkIfNotEmpty(TextBox txt)
        {
            CancelEventArgs x = new CancelEventArgs();
            if (string.IsNullOrEmpty(txt.Text))
            {
                x.Cancel = true;
                errorProvider1.SetError(txt, "This field should not be Empty");
                return false;
            }
            else
            {
                x.Cancel = false;
                errorProvider1.SetError(txt, "");
                return true;
            }
        }
        private bool isConfirmPasswordValid()
        {
            CancelEventArgs x = new CancelEventArgs();
            if (string.IsNullOrEmpty(txtConfirmPassword.Text) || txtConfirmPassword.Text != txtPassword.Text)
            {
                x.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Inputs aren't valid");
                return false;
            }
            else
            {
                x.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
                return true;
            }
        }
        private clsUser FillNewUserInfo()
        {
            clsUser User = new clsUser();
            User.PersonID = Convert.ToInt16(personInfoCardWithFilter1.PersonID);
            User.UserName = txtUsername.Text;
            User.Password = txtPassword.Text;
            if (chIsActive.Checked == true)
            {
                User.isActive = 1;
            }
            else
            {
                User.isActive = 0;
            }
            return User;
        }
        private void UpdateUserInfo(ref clsUser User)
        {
            User.PersonID = Convert.ToInt16(personInfoCardWithFilter1.PersonID);
            User.UserName = txtUsername.Text;
            User.Password = txtPassword.Text;
            if (chIsActive.Checked == true)
            {
                User.isActive = 1;
            }
            else
            {
                User.isActive = 0;
            }
        }
        private void ChangeTitle()
        {
            if (_User.UserID==-1)
            {
                lbModeTitle.Text = "Add New User";
            }
            else
            {
                lbModeTitle.Text = "Update User";
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (checkIfNotEmpty(txtUsername)==false|| checkIfNotEmpty(txtPassword) == false || isConfirmPasswordValid()==false)
            {
                return;
            }
            _User = clsUser.FindUserByPersonID(personInfoCardWithFilter1.PersonID);
            if (_User.UserID!=-1)
            {
                UpdateUserInfo(ref _User);
                if (_User.Save())
                {
                    MessageBox.Show("Updated Successfully");
                }
                else
                {
                    MessageBox.Show("Update Failed");
                }
            }
            else
            {
                _User = FillNewUserInfo();
                if (_User.Save())
                {
                    MessageBox.Show("Added Successfully");
                    lbUserID.Text = clsUser.FindUserByPersonID(_User.PersonID).UserID.ToString();
                    lbModeTitle.Text = "Update User";
                }
                else
                {
                    MessageBox.Show("Adding Failed");
                }
            }
        }
    }
    
}
