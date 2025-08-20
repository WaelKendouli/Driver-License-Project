using PeopleBussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsersBussinessLayer;

namespace Driver_Licence_Project
{
    public partial class UsersManagementForm : Form
    {
        public UsersManagementForm()
        {
            InitializeComponent();
        }

        private void _RefreshUsersList()
        {
            dgvUsers.DataSource = clsUser.GetAllUsers();
        }
        private void UsersManagementForm_Load(object sender, EventArgs e)
        {
            cbActives.Visible = false;
            cbActives.Items.Add("All");
            cbActives.Items.Add("Yes");
            cbActives.Items.Add("No");
            txtFilter.Visible = false;
            _RefreshUsersList();
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("UserID");
            cbFilter.Items.Add("PersonID");
            cbFilter.Items.Add("UserName");
            cbFilter.Items.Add("Password");
            cbFilter.Items.Add("IsActive");


        }

        private void _HandlingIsActive()
        {
            if (cbFilter.SelectedIndex == cbFilter.FindString("isActive"))
            {
                txtFilter.Visible = false;
                cbActives.Visible = true;
                

            }
            else
            {
                txtFilter.Visible = true;
                cbActives.Visible = false;
            }
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
           


            if ((txtFilter.Text == "") || cbFilter.Text == "None")
            {
                _RefreshUsersList();
                return;
            }

            

            if ((cbFilter.SelectedIndex == cbFilter.FindString("UserID") || cbFilter.SelectedIndex == cbFilter.FindString("PersonID")) && Regex.IsMatch(txtFilter.Text, @"[a-zA-Z]"))
            {
                txtFilter.Clear();
                return;
            }

            dgvUsers.DataSource = clsUser.FilterContentBy(cbFilter.Text, txtFilter.Text);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _HandlingIsActive();
            if (cbFilter.Text != "None")
            {
                txtFilter.Visible = true;
            }
            else
            {
                txtFilter.Visible = false;
                txtFilter.Clear();
                _RefreshUsersList();
            }
        }

        private void cbActives_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbActives.SelectedIndex == cbActives.FindString("Yes"))
            {
                dgvUsers.DataSource = clsUser.GetAllActiveAndnonActiveUsers(1);
            }
            else if (cbActives.SelectedIndex == cbActives.FindString("No"))
            {
                dgvUsers.DataSource = clsUser.GetAllActiveAndnonActiveUsers(0);

            }
            else
            {
                _RefreshUsersList();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddEditUsers frm = new frmAddEditUsers();
            
            frm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonDetails frm = new frmShowPersonDetails((int)dgvUsers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsUser.DeleteUser((int)dgvUsers.CurrentRow.Cells[1].Value)==true)
            {
                MessageBox.Show("Deleted Successfully");
                dgvUsers.DataSource = clsUser.GetAllUsers();
            }
            else
            {
                MessageBox.Show("Deletion Failed");
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUsers frm = new frmAddEditUsers((int)dgvUsers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUsers frm = new frmAddEditUsers();
            frm.ShowDialog();
        }
    }
}
