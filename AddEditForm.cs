using PeopleBussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_Licence_Project
{
    public partial class frmAddEdit : Form
    {
        public frmAddEdit()
        {
            InitializeComponent();
        }
       public static clsPerson _Person = new clsPerson();


        public frmAddEdit( ref clsPerson EditPerson)
        {
            InitializeComponent();
             _Person = EditPerson;
            personInfo1.PersonIDVal = _Person.PersonID;
            personInfo1.txtFirstNameVal = _Person.FirstName;
            personInfo1.txtSecondNameVal = _Person.SecondName;
            personInfo1.txtThirdNameVal = _Person.ThirdName;
            personInfo1.txtLastNameVal = _Person.LastName;
            personInfo1.txtNationalNoVal = _Person.NationalNo;
            personInfo1.txtPhoneVal = _Person.Phone;
            personInfo1.txtEmailVal = _Person.Email;
            personInfo1.CountryIDVal = _Person.NationalityCountryID;
            personInfo1.DateTimeVal = _Person.DateOfBirth;
            personInfo1.ImagePathVal = _Person.ImagePath;
            personInfo1.rtbAddressVal = _Person.Address;
            personInfo1.GendorVal = _Person.Gendor;
        }
        public frmAddEdit(ref int PersonID)
        {
            InitializeComponent();
            _Person = clsPerson.FindPersonByID(PersonID);

            if (_Person!=null)
            {
                personInfo1.PersonIDVal = _Person.PersonID;
                personInfo1.txtFirstNameVal = _Person.FirstName;
                personInfo1.txtSecondNameVal = _Person.SecondName;
                personInfo1.txtThirdNameVal = _Person.ThirdName;
                personInfo1.txtLastNameVal = _Person.LastName;
                personInfo1.txtNationalNoVal = _Person.NationalNo;
                personInfo1.txtPhoneVal = _Person.Phone;
                personInfo1.txtEmailVal = _Person.Email;
                personInfo1.CountryIDVal = _Person.NationalityCountryID;
                personInfo1.DateTimeVal = _Person.DateOfBirth;
                personInfo1.ImagePathVal = _Person.ImagePath;
                personInfo1.rtbAddressVal = _Person.Address;
                personInfo1.GendorVal = _Person.Gendor;
            }
        }
        public delegate void frmAddEditDataHandler(object sender, DataTable dt);

        public event frmAddEditDataHandler DataHandler;

        public delegate void AddEditPersonDataBack(object sender, int PersonID);

        public event AddEditPersonDataBack PersonDataBack; // Used in Add/Edit Person with filter

        private void button1_Click(object sender, EventArgs e) // Close Button
        {
            DataTable dt = clsPerson.GetAllPeople();

            DataHandler?.Invoke(this, dt);

            int PersonID = clsPerson.GetPersonID(personInfo1.txtNationalNoVal);
            this.DialogResult = DialogResult.OK;
            PersonDataBack?.Invoke(this, PersonID);

            this.Close();
        }

        private void personInfo1_Load(object sender, EventArgs e)
        {

        }

        private void frmAddEdit_Load(object sender, EventArgs e)
        {
            if (personInfo1.PersonIDVal == -1)
            {
                lTitle.Text = "Add new Person";
            }
            else
            {
                lTitle.Text = "Update Person";
            }

        }
    }
}
