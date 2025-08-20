using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestTypesBusinessLayer;

namespace Driver_Licence_Project
{
    public partial class frmEditTestType : Form
    {
        private int _PersonID;
        clsTestType Test = new clsTestType();
        public frmEditTestType(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
             Test = clsTestType.FindTestTypeByID(_PersonID);
            if (Test!=null)
            {
                lbtestTypeID.Text = Test.TestTypeID.ToString();
                txtNewTitle.Text = Test.TestTypeTitle;
                txtNewFees.Text = Test.TestTypeFees.ToString();
                txtDescription.Text = Test.TestTypeDescription;
            }
        }
        private bool AreInputsIncorrect()
        {
            return (string.IsNullOrEmpty(txtNewTitle.Text.Trim()) || string.IsNullOrEmpty(txtNewFees.Text.Trim()) || string.IsNullOrEmpty(txtDescription.Text.Trim()) );
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (AreInputsIncorrect())
            {
                MessageBox.Show("Inputs Invalid");
                return;
            }
            Test.TestTypeTitle = txtNewTitle.Text.Trim();
            Test.TestTypeFees = Convert.ToSingle(txtNewFees.Text.Trim());
            Test.TestTypeDescription = txtDescription.Text.Trim();
            if (Test.Save())
            {
                MessageBox.Show("Updated Successfully");

            }
            else
            {
                MessageBox.Show("Updating Failed");

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
