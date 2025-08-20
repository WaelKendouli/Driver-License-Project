using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using PeopleBussinessLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Driver_Licence_Project
{
    public partial class frmPeopleMangement : Form
    {
        
        public frmPeopleMangement()
        {
            InitializeComponent();
        }

        

        private  void _RefreshDataGrid()
        {
            dgvPeople.DataSource = clsPerson.GetAllPeople();
        }
        private void frmPeopleMangement_Load(object sender, EventArgs e)
        {
          

            txtFilter.Visible = false;
            _RefreshDataGrid();
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("PersonID");
            cbFilter.Items.Add("NationalNo");
            cbFilter.Items.Add("FirstName");
            cbFilter.Items.Add("SecondName");
            cbFilter.Items.Add("ThirdName");
            cbFilter.Items.Add("LastName");
            cbFilter.Items.Add("Gendor");
            cbFilter.Items.Add("Phone");
            cbFilter.Items.Add("Email");

            UpdateNumberOfRecords();

        }
        private void UpdateNumberOfRecords()
        {
            lbNumOfRecords.Text = dgvPeople.RowCount.ToString();
        }
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text!="None")
            {
                txtFilter.Visible = true;
            }
            else
            {
                txtFilter.Visible = false;
                txtFilter.Clear();
                _RefreshDataGrid();
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if ((txtFilter.Text =="") || cbFilter.Text == "None")
            {
                return;
            }

            if ((cbFilter.SelectedIndex == cbFilter.FindString("PersonID")|| cbFilter.SelectedIndex == cbFilter.FindString("Gendor")|| cbFilter.SelectedIndex == cbFilter.FindString("Phone")) && Regex.IsMatch(txtFilter.Text, @"[a-zA-Z]"))
            {
                txtFilter.Clear();
                return;
            }

            if ((cbFilter.SelectedIndex == cbFilter.FindString("FirstName") || cbFilter.SelectedIndex == cbFilter.FindString("LastName") || cbFilter.SelectedIndex == cbFilter.FindString("ThirdName") || cbFilter.SelectedIndex == cbFilter.FindString("SecondName")) && Regex.IsMatch(txtFilter.Text, @"\d"))
            {
                txtFilter.Clear();
                return;
            }

            dgvPeople.DataSource =  clsPerson.GetListPeopleFilteredBy(cbFilter.Text, txtFilter.Text.ToUpper());
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddEdit frm = new frmAddEdit();
            frm.DataHandler += frmAddEditDataIsBack;
            frm.ShowDialog();
            UpdateNumberOfRecords();
        }
        private void frmAddEditDataIsBack(object sender , DataTable dt)
        {
            dgvPeople.DataSource = dt;
            
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEdit frm = new frmAddEdit();
            frm.DataHandler += frmAddEditDataIsBack;
            frm.ShowDialog();
            UpdateNumberOfRecords();
        }


        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsPerson.DeletePerson((int)dgvPeople.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Deleted Successfully");
                _RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Deletion Failed");

            }
            UpdateNumberOfRecords();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson Person = clsPerson.FindPersonByID((int)dgvPeople.CurrentRow.Cells[0].Value);
            frmAddEdit frm = new frmAddEdit(ref Person);
            frm.DataHandler += frmAddEditDataIsBack;
            frm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson Person = clsPerson.FindPersonByID((int)dgvPeople.CurrentRow.Cells[0].Value);
            frmShowPersonDetails frm = new frmShowPersonDetails(ref Person);
            frm.ShowDialog();
        }
    }
}