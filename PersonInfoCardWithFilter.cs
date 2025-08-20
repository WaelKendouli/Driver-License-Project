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
    public partial class PersonInfoCardWithFilter : UserControl
    {
        public PersonInfoCardWithFilter()
        {
            InitializeComponent();
        }

        public event Action<int> OnPersonSelected;

        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler!=null)
            {
                handler(PersonID);
            }

        }

        private bool _ShowAddPerson = true;

        public bool ShowAddPerson
        {
            get { return _ShowAddPerson; }
            set
            {
                _ShowAddPerson = value;
                btnAdd.Visible = _ShowAddPerson;
            }
        }

        private bool _ShowFilter  = true;
        public bool ShowFilter
        {
            get { return _ShowFilter; }
            set
            {
                _ShowFilter = value;
                groupBox1.Enabled = _ShowFilter;
            }
        }

        private int _PersonID = -1;

        public int PersonID
        {
            get { return ucPersonInformationCard1.PersonIDVal; }
            set { 
                _PersonID = value;

            }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ucPersonInformationCard1.SelectedPerson ; }
        }
        public void ResetInfosValues()
        {
            ucPersonInformationCard1.ResetInfosctrl();
        }

        private void _LoadPersonInfo(int PersonID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilter.Text = PersonID.ToString();
            FindNow();
        }
        private void FindNow()
        {
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    ucPersonInformationCard1.LoadInfosCard(int.Parse(txtFilter.Text));
                    break;
                case "National No":
                    ucPersonInformationCard1.LoadInfosCard(txtFilter.Text);
                    break;

            }
            if (OnPersonSelected!=null&&ShowFilter)
            {
                OnPersonSelected(ucPersonInformationCard1.PersonIDVal);
            }


        }

        public void LoadPersonInfo(int PersonID)
        {
            ucPersonInformationCard1.LoadInfosCard(PersonID);
        }

        private void PersonInfoCardWithFilter_Load(object sender, EventArgs e)
        {
            ResetInfosValues();
            cbFilterBy.Items.Add("Person ID");
            cbFilterBy.Items.Add("National No");
 
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                MessageBox.Show("Inputs are invalid");
                return;
            }
            FindNow();
        }



        private void DataBack(object sender, int PersonID)
        {
  
            _PersonID = PersonID;
            ucPersonInformationCard1.PersonIDVal = PersonID;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (frmAddEdit frm = new frmAddEdit())
            {
                frm.PersonDataBack += DataBack;

                // Wait for dialog to close
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // The event should have fired before dialog closed
                    cbFilterBy.SelectedIndex = 1;
                    txtFilter.Text = _PersonID.ToString();
                    ucPersonInformationCard1.LoadInfosCard(_PersonID);
                }
            }
        }
    }
}
