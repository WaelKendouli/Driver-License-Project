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
using UsersBussinessLayer;
using static System.Net.Mime.MediaTypeNames;

namespace Driver_Licence_Project
{
    public partial class frmChangePassword : Form
    {
        private int _PersonID;
        clsUser User = new clsUser();
        public frmChangePassword(int PersonID)
        {
      
            InitializeComponent();
            User = clsUser.FindUserByPersonID(PersonID);
            ctrlPasswordChange1._PersonID = PersonID;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
         // ctrlPasswordChange1._PersonID = _PersonID;   


        }
        private bool isTextBoxEmpty(TextBox txt)
        {
            CancelEventArgs e = new CancelEventArgs();
            if (string.IsNullOrEmpty(txt.Text))
            {
                e.Cancel = true;
                txt.Focus();
                errorProvider1.SetError(txt, "this field should not be Empty");
                return true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt, "");
                return false;
            }
        }
        private bool isInputsEmpty()
        {
            return (isTextBoxEmpty(txtOldPassword) || isTextBoxEmpty(txtNewPassword) || isTextBoxEmpty(txtConfirmNew));
        }

        private bool IsPasswordRepeated()
        {
            CancelEventArgs e = new CancelEventArgs();
            if (txtNewPassword.Text.Trim() == User.Password.Trim())
            {
                e.Cancel = true;
                txtNewPassword.Focus();
                errorProvider1.SetError(txtNewPassword, " new password should not be the same as the old one");
                return true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNewPassword, "");
                return false;
            }
        }

        private bool isPasswordWrong()
        {
            CancelEventArgs e = new CancelEventArgs();
            if (txtOldPassword.Text.Trim() != User.Password.Trim())
            {
                e.Cancel = true;
                txtNewPassword.Focus();
                errorProvider1.SetError(txtOldPassword, " Password incorrect");
                return true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtOldPassword, "");
                return false;
            }
        }
        private bool isConfirmPasswordWrong()
        {
            CancelEventArgs e = new CancelEventArgs();
            if (txtConfirmNew.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                txtNewPassword.Focus();
                errorProvider1.SetError(txtConfirmNew, " Password incorrect");
                return true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmNew, "");
                return false;
            }
        }
        private bool CheckIfInputsAreCorrect()
        {
            return (isInputsEmpty() || isPasswordWrong() || IsPasswordRepeated() || isConfirmPasswordWrong());
        }
        private void ctrlPasswordChange1_Load(object sender, EventArgs e)
        {

        }

        private void btnSavePassword_Click(object sender, EventArgs e)
        {
            if (CheckIfInputsAreCorrect())
            {
                return;
            }
            User.Password = txtNewPassword.Text;
            if (User.Save())
            {
                MessageBox.Show("Password updated successfully");
            }
            else
            {
                MessageBox.Show("Updating password failed");
            }
        }
    }
}
