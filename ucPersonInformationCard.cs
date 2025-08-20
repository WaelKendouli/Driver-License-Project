using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CountriesBussinessLayer;
using PeopleBussinessLayer;

namespace Driver_Licence_Project
{
    public partial class ucPersonInformationCard : UserControl
    {
        public static clsPerson _Person = new clsPerson();

        public clsPerson SelectedPerson
        { get { return _Person; }
        }
        private int _PersonID;
        public int PersonIDVal
        {
            get {
                
                    return _PersonID;
            }
            set { _PersonID = value; }
        }
        public string NameVal
        {
            get { return lbName.Text; }
            set { lbName.Text = value; }
        }


        public string NationalNoVal
        {
            get { return lbNationalNo.Text; }
            set { lbNationalNo.Text = value; }
        }

        public string PhoneVal
        {
            get { return lbPhone.Text; }
            set { lbPhone.Text = value; }
        }

        public string EmailVal
        {
            get { return lbEmail.Text; }
            set { lbEmail.Text = value; }
        }

        public string AddressVal
        {
            get { return lbAddress.Text; }
            set { lbAddress.Text = value; }
        }

        public string DateTimeVal
        {
            get { return lbDateOfBirth.Text; }
            set { lbDateOfBirth.Text = value; }
        }

        public string CountryIDVal
        {
            get { return lbCountry.Text; }
            set
            {
                lbCountry.Text = value;
            }
        }
        public string ImagePathVal
        {

            get
            {
                string SortedPath = pbPersonImage?.Tag.ToString();
                return SortedPath;
            }
            set { pbPersonImage.ImageLocation = value; }
        }

        public string GendorVal
        {
            get
            {
                return lbGender.Text;
            }
            set
            {
                lbGender.Text = value;
            }
        }


        public ucPersonInformationCard()
        {
            InitializeComponent();
        }
        public ucPersonInformationCard(ref clsPerson Person)
        {
            if (Person!=null)
            {
               InitializeComponent();
            _Person = Person;
                _PersonID = Person.PersonID;
            }
            
        }
        public void ResetInfosctrl()
        {
            lbName.Text = "[????]";
             lbEmail.Text = "[????]";
                lbGender.Text = "[????]";
            lbCountry.Text = "[????]";
            lbDateOfBirth.Text = "[????]";
            lbPhone.Text = "[????]";
            LbiID.Text = "[????]";
            lbAddress.Text = "[????]";
            lbNationalNo.Text = "[????]";
            pbPersonImage.ImageLocation = "";
        }
        private void _LoadPersonImage(clsPerson Person)
        {
            if (Person.Gendor == 0)
            {
                pbPersonImage.Image = Properties.Resources.MaleUser;
            }
            else
            {
                pbPersonImage.Image = Properties.Resources.FemalUser;
            }
            string ImagePath = Person.ImagePath;
            if (ImagePath!="")
            {
                if (File.Exists(ImagePath))
                {
                    pbPersonImage.ImageLocation = ImagePath;
                }
                else
                {
                    MessageBox.Show("File with path "+ImagePath+" Doesn't Exist ","Error", MessageBoxButtons.OK ,MessageBoxIcon.Information);
                }
            }


        }
        public void LoadInfosCard(clsPerson Person)
        {

            if (Person != null)
            {
                LbiID.Text = Person.PersonID.ToString();
                _PersonID = Person.PersonID;
                lbName.Text = Person.FirstName + " " + Person.SecondName + " " + Person.ThirdName + " " + Person.LastName;
                lbEmail.Text = Person.Email;
                if (Person.Gendor == 0)
                {
                    lbGender.Text = "Male";
                }
                else
                {
                    lbGender.Text = "Female";
                }
                lbCountry.Text = clsCountry.FindCountryName(Person.NationalityCountryID);
                lbDateOfBirth.Text = Person.DateOfBirth.ToString();
                lbPhone.Text = Person.Phone;
                LbiID.Text = Person.PersonID.ToString();
                lbAddress.Text = Person.Address;
                lbNationalNo.Text = Person.NationalNo;
                _LoadPersonImage(Person);
            }

        }

        public void LoadInfosCard(int PersonID)
        {
            clsPerson Person = clsPerson.FindPersonByID(PersonID);
            if (Person!=null)
            {
            LbiID.Text = Person.PersonID.ToString();
                _PersonID = Person.PersonID;
                lbName.Text = Person.FirstName + " " + Person.SecondName + " " + Person.ThirdName + " " + Person.LastName;
            lbEmail.Text = Person.Email;
            if (Person.Gendor == 0)
            {
                lbGender.Text = "Male";
            }
            else
            {
                lbGender.Text = "Female";
            }
            lbCountry.Text = clsCountry.FindCountryName(Person.NationalityCountryID);
            lbDateOfBirth.Text = Person.DateOfBirth.ToString();
            lbPhone.Text = Person.Phone;
            LbiID.Text = Person.PersonID.ToString();
            lbAddress.Text = Person.Address;
            lbNationalNo.Text = Person.NationalNo;
            _LoadPersonImage(Person);    
            }

            
        }

        public void LoadInfosCard(string NationalNo)
        {
            clsPerson Person = new clsPerson();
            Person = clsPerson.FindPersonByNationalNo(NationalNo);
            if (Person != null)
            {
                LbiID.Text = Person.PersonID.ToString();
                _PersonID = Person.PersonID;
                lbName.Text = Person.FirstName + " " + Person.SecondName + " " + Person.ThirdName + " " + Person.LastName;
                lbEmail.Text = Person.Email;
                if (Person.Gendor == 0)
                {
                    lbGender.Text = "Male";
                }
                else
                {
                    lbGender.Text = "Female";
                }
                lbCountry.Text = clsCountry.FindCountryName(Person.NationalityCountryID);
                lbDateOfBirth.Text = Person.DateOfBirth.ToString();
                lbPhone.Text = Person.Phone;
                LbiID.Text = Person.PersonID.ToString();
                lbAddress.Text = Person.Address;
                lbNationalNo.Text = Person.NationalNo;
                _LoadPersonImage(Person);
            }
        }

        private void ucPersonInformationCard_Load(object sender, EventArgs e)
        {
            if (_Person.PersonID != -1&& _Person!=null)
            {
              LoadInfosCard(_Person);  
            }
           

        }

        private void LbiID_Click(object sender, EventArgs e)
        {

        }

        private void gbPersonInfo_Enter(object sender, EventArgs e)
        {

        }

        private void LLEditInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            frmAddEdit frm = new frmAddEdit(ref _PersonID);

            frm.ShowDialog();
            
        }
    }
}
