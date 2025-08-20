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
using System.IO;
namespace Driver_Licence_Project
{
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        public delegate void LoginScreenDataHandler(object sender, int ID);

        public event LoginScreenDataHandler DataHandler;

        private clsUser _User = new clsUser();
        private string FileName = "RememberMe.txt";
        private string FileContent;
        private void LoginScreen_Load(object sender, EventArgs e)
        {
            if (File.Exists(FileName))
            { 
                FileContent = File.ReadAllText(FileName);
                if (FileContent.Contains('*'))  
            {
               string[] Remebering = FileContent.Split('*');
                    txtUserName.Text = Remebering[0];
                    txtPassword.Text = Remebering[1];
            }
            }  
        }

        private bool _CheckLogIn()
        {
            clsUser User = clsUser.FindUserByName(txtUserName.Text);
            if (User == null)
            {
                MessageBox.Show("User Doesn't Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (clsUser.IsUserHavePermission(txtUserName.Text , txtPassword.Text , User))
            {
                _User = User;
                if (User.isUserActive())
                {
                    return true;   
                }
                else
                {
                    MessageBox.Show("your account is disactivated please contact your Manager","Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Invalid Password / User Name","Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtUserName.Text)||string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please Make sure to fill all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (chbRemeberMe.Checked)
            {
                if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    FileContent = " ";
                    File.WriteAllText(FileName, FileContent);
                }
                else
                {
                FileContent = txtUserName.Text + '*' + txtPassword.Text;
                File.WriteAllText(FileName, FileContent);   
                }
                
            }
            else
            {
                FileContent = " ";
                File.WriteAllText(FileName, FileContent);
            }



            if (_CheckLogIn()==true)
            {
                this.DialogResult = DialogResult.OK;
                if (_User!=null)
                {
                    DataHandler?.Invoke(this, _User.PersonID);
                }

                this.Close();

            }

        }
    }
}
