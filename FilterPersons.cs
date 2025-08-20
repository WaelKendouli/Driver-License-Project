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

namespace Driver_Licence_Project
{
    public partial class FilterPersons : UserControl
    {
        public FilterPersons()
        {
            InitializeComponent();
        }
        private clsPerson _EmptyPerson = new clsPerson();
        public clsPerson PersonVal
        { get { return _EmptyPerson; }
            set { _EmptyPerson = value; }
        }

        public event EventHandler<clsPerson> PersonFound;



        private void FilterPersons_Load(object sender, EventArgs e)
        {
            txtFilter.Visible = false;
            cbFilterBy.Items.Add("None");
            cbFilterBy.Items.Add("PersonID");
            cbFilterBy.Items.Add("NationalNo");
            cbFilterBy.Items.Add("FirstName");
            cbFilterBy.Items.Add("SecondName");
            cbFilterBy.Items.Add("ThirdName");
            cbFilterBy.Items.Add("LastName");
            cbFilterBy.Items.Add("Gendor");
            cbFilterBy.Items.Add("Phone");
            cbFilterBy.Items.Add("Email");
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text != "None")
            {
                txtFilter.Visible = true;
            }
            else
            {
                txtFilter.Visible = false;
                txtFilter.Clear();
            }
        }
        private void HandelEdgeCasesForInputs()
        {
            if ((txtFilter.Text == "") || cbFilterBy.Text == "None")
            {
                return;
            }

            if ((cbFilterBy.SelectedIndex == cbFilterBy.FindString("PersonID") || cbFilterBy.SelectedIndex == cbFilterBy.FindString("Gendor") || cbFilterBy.SelectedIndex == cbFilterBy.FindString("Phone")) && Regex.IsMatch(txtFilter.Text, @"[a-zA-Z]"))
            {
                txtFilter.Clear();
                return;
            }

            if ((cbFilterBy.SelectedIndex == cbFilterBy.FindString("FirstName") || cbFilterBy.SelectedIndex == cbFilterBy.FindString("LastName") || cbFilterBy.SelectedIndex == cbFilterBy.FindString("ThirdName") || cbFilterBy.SelectedIndex == cbFilterBy.FindString("SecondName")) && Regex.IsMatch(txtFilter.Text, @"\d"))
            {
                txtFilter.Clear();
                return;
            }
           
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            HandelEdgeCasesForInputs();
            if (txtFilter.Text.Length < 2) 
            {
                return;
            }
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
             PersonVal = clsPerson.GetSpecificPersonByFilter(cbFilterBy.Text, txtFilter.Text);
            if (PersonVal.PersonID!=-1)
            {
                PersonFound?.Invoke(this, PersonVal);
            }
            else
            {
                MessageBox.Show("not found in database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
