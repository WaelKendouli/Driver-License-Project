using PeopleBussinessLayer;
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
using UtilityClass;
using static System.Net.Mime.MediaTypeNames;


namespace Driver_Licence_Project
{
    public partial class PersonInfo : UserControl
    {
        public PersonInfo()
        {
            InitializeComponent();
            dtpDate.MinDate = new DateTime(1951, 1, 1);
            dtpDate.MaxDate = DateTime.Now.AddDays(3);
        

        }

        public int PersonIDVal
        {
            get {
                if (string.IsNullOrEmpty(lbPersonID.Text))
                {
                    return -1;
                }
                else
                {
                 return Convert.ToInt32(lbPersonID.Text);   
                }
                
            }
            set { lbPersonID.Text = value.ToString(); }
        }
        public string txtFirstNameVal
        {
            get { return txtFirstName.Text; }
            set { txtFirstName.Text = value; }
        }

        public string txtLastNameVal
        {
            get { return txtLast.Text; }
            set { txtLast.Text = value; }
        }
        public string txtSecondNameVal
        {
            get { return txtSecond.Text; }
            set { txtSecond.Text = value; }
        }
        public string txtThirdNameVal
        {
            get { return txtThird.Text; }
            set { txtThird.Text = value; }
        }

        public string txtNationalNoVal
        {
            get { return txtNationalNo.Text; }
            set { txtNationalNo.Text = value; }
        }

        public string txtPhoneVal
        {
            get { return txtPhone.Text; }
            set { txtPhone.Text = value; }
        }

        public string txtEmailVal
        {
            get { return txtEmail.Text; }
            set { txtEmail.Text = value; }
        }

        public string rtbAddressVal
        {
            get { return rtbAddress.Text; }
            set { rtbAddress.Text = value; }
        }

        public DateTime DateTimeVal
        {
            get {return dtpDate.Value ; }
            set { dtpDate.Value = DateTime.Now ;}
        }

        public int CountryIDVal
        {
            get { return cbCountries.SelectedIndex; }
            set
            {
                cbCountries.SelectedValue = value;
            }
        }
        public string ImagePathVal
        {
            
            get { string SortedPath = pbPersonImage.ImageLocation;
                return SortedPath;
            }
            set {pbPersonImage.ImageLocation = value; }
        }

        public int GendorVal
        {
            get {
                if (rbMale.Checked)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            set { if (value == 1)
                {
                    rbFemale.Checked = true;  
                }
                else
                {
                    rbMale.Checked = true;
                }
            }
        }

        private bool _IsCustomPic = false;
        private  DataTable Countries = clsPerson.GetAllCountries();
        private bool CheckIfNotEmpty(TextBox txt)
        {
            CancelEventArgs e = new CancelEventArgs();
            if (string.IsNullOrEmpty(txt.Text))
            {
                e.Cancel = true;
                txt.Focus();
                errorProvider1.SetError(txt, $"[{txt.Name}] should not be empty");
                return false;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt, "");
                return false;
            }
        }

        private bool CheckIfNotEmpty(RichTextBox txt)
        {
            CancelEventArgs e = new CancelEventArgs();
            if (string.IsNullOrEmpty(txt.Text))
            {
                e.Cancel = true;
                txt.Focus();
                errorProvider1.SetError(txt, $"{txt.Name} should not be empty");
                return false;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt, "");
                return true;
            }
        }

        private bool CheckIfNumber(TextBox txt)
        {
            CancelEventArgs e = new CancelEventArgs();
            if ((int.TryParse(txt.Text, out int result)))
            {
                e.Cancel = true;
                txt.Focus();
                errorProvider1.SetError(txt, "this field should not be numeric ");
                return true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt, "");
                return false;
            }

        }

        private bool CheckIfEmailIsValid(TextBox txt)
        {
            CancelEventArgs e = new CancelEventArgs();
            if (!(txt.Text.Contains('@')))
            {
                e.Cancel = true;
                txt.Focus();
                errorProvider1.SetError(txt, "this field should Contain @ ");
                return false;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt, "");
                return true;
            }

        }

        private void ChangePhoto()
        {
            if (rbMale.Checked == true && _IsCustomPic == false)
            {
                pbPersonImage.Image = Properties.Resources.MaleUser;
            }
            if (rbFemale.Checked == true && _IsCustomPic == false)
            {
                pbPersonImage.Image = Properties.Resources.FemalUser;

            }
        }

        private clsPerson FillUpdatePersonInfo(clsPerson Person)
        {
             
            Person.FirstName = txtFirstName.Text;
            Person.LastName = txtLast.Text;
            Person.Email = txtEmail.Text;
            Person.Phone = txtPhone.Text;
            if (rbMale.Checked == true)
            {
                Person.Gendor = 0;
            }
            else
            {
                Person.Gendor = 1;

            }

            Person.SecondName = txtSecond.Text;

            if (string.IsNullOrEmpty(txtThird.Text))
            {
                Person.ThirdName = "";
            }
            else
            {
                Person.ThirdName = txtThird.Text;
            }
            Person.NationalNo = txtNationalNo.Text;

            foreach (DataRow Row in Countries.Rows)
            {
                if (Row["CountryName"] == cbCountries.SelectedItem)
                {
                    Person.NationalityCountryID = Convert.ToInt32(Row["CountryID"]);
                    break;
                }
            }
            Person.Address = rtbAddress.Text;
            if (pbPersonImage.ImageLocation != null)
            {
                Person.ImagePath = pbPersonImage.ImageLocation;
            }
            else
            {
                Person.ImagePath = "";
            }
            Person.DateOfBirth = dtpDate.Value;

            return Person;
        }
        private clsPerson AddNewPersonInfo()
        {
            clsPerson NewPerson = new clsPerson();
            NewPerson.FirstName = txtFirstName.Text;
            NewPerson.LastName = txtLast.Text;
            NewPerson.Email = txtEmail.Text;
            NewPerson.Phone = txtPhone.Text;
            if (rbMale.Checked==true)
            {
                NewPerson.Gendor = 0;
            }
            else
            {
                NewPerson.Gendor = 1;

            }
          
                NewPerson.SecondName = txtSecond.Text;
            
            if (string.IsNullOrEmpty(txtThird.Text))
            {
                NewPerson.ThirdName = "";
            }
            else
            {
                NewPerson.ThirdName = txtThird.Text;
            }
            NewPerson.NationalNo = txtNationalNo.Text;

            foreach (DataRow Row in Countries.Rows)
            {
                if (Row["CountryName"]==cbCountries.SelectedItem)
                {
                    NewPerson.NationalityCountryID = Convert.ToInt32(Row["CountryID"]);
                    break;
                }
            }
            NewPerson.Address = rtbAddress.Text;
            if (pbPersonImage.ImageLocation!=null)
            {
                NewPerson.ImagePath = pbPersonImage.ImageLocation;
            }
            else
            {
                NewPerson.ImagePath = "";
            }
            NewPerson.DateOfBirth = dtpDate.Value;

            return NewPerson;
        }

        private void _FillComboBox()
        {
            

            foreach (DataRow Row in Countries.Rows)
            {
                cbCountries.Items.Add(Row["CountryName"]);
            }
        }
        private void PersonInfo_Load(object sender, EventArgs e)
        {
            _FillComboBox();
            int Index = cbCountries.FindString("Algeria");
            cbCountries.SelectedIndex = Index;
            rbMale.Checked = true;
            dtpDate.MaxDate = DateTime.Now.AddYears(-18);
            pbPersonImage.Image = Properties.Resources.MaleUser;
            llRemoveImage.Visible = (pbPersonImage.Image != null);
            _IsCustomPic = (ImagePathVal == ""); 
        }

        private bool CheckIfRequiredInputsAreFull()
        {
            return (CheckIfNotEmpty(txtFirstName)|| CheckIfNotEmpty(txtLast) || CheckIfNotEmpty(txtNationalNo)
                || CheckIfNotEmpty(txtPhone) || CheckIfNotEmpty(txtEmail) || CheckIfNotEmpty(rtbAddress) || CheckIfNotEmpty(txtSecond));
        }

        private bool CheckIfNationalNoIsRepeatedAfterUpdate()
        {
            CancelEventArgs e = new CancelEventArgs();
            if (clsPerson.isPersonExist(txtNationalNo.Text))
            {
                e.Cancel = true;
                txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "National Number is Repeated , Choose an other");
                return true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, "");
                return false;
            }
        }

        private bool CheckIfNationalNoIsRepeated()
        {
            CancelEventArgs e = new CancelEventArgs();
            if (clsPerson.isPersonExist(txtNationalNo.Text) && PersonIDVal == -1)
            {
                e.Cancel = true;
                txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "National Number is Repeated , Choose an other");
                return true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, "");
                return false;
            }
        }

        private bool CheckIfNumericValues() // this function is used to unsure that all these field are not numeric values 
        {
            return (CheckIfNumber(txtFirstName)||CheckIfNumber(txtLast)||CheckIfNumber(txtSecond)||CheckIfNumber(txtThird));
        }

        private bool _HandelImagePerson(clsPerson Person)
        {
            if (Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (Person.ImagePath!="")
                {
                    try
                    {
                        File.Delete(Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        
                    }

                }


            }
            if (pbPersonImage.ImageLocation!=null)
            {
                string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                {
                    pbPersonImage.ImageLocation = SourceImageFile;
                    return true;
                }
                else
                {
                    MessageBox.Show("Error Copying a file","Error",MessageBoxButtons.OK , MessageBoxIcon.Error );
                    return false;
                }

            }
            return true;
        }

        private void Save()
        {
            if (CheckIfRequiredInputsAreFull() == false)
            {
                return;
            }
            if (CheckIfNumericValues() == true)
            {
                return;
            }
            if (CheckIfEmailIsValid(txtEmail) == false)
            {
                return;
            }
            if (CheckIfNationalNoIsRepeated()==true)
            {
                return; 
            }

            if (PersonIDVal==-1)
            {
              clsPerson NewPerson = AddNewPersonInfo(); 
            if (NewPerson.Save())
            {
                MessageBox.Show("Added Successfully");
                 //   _HandelImagePerson(NewPerson);
            }
            else
            {
                MessageBox.Show("Adding failed , National number may be repeated");
            } 

            }
            else
            {
               clsPerson UpdatedPerson = FillUpdatePersonInfo(frmAddEdit._Person);
                if (CheckIfNationalNoIsRepeatedAfterUpdate() == true)
                {
      
                    return;
                }
                if (UpdatedPerson.Save())
                {
                    MessageBox.Show("Updated Successfully");
                  //  _HandelImagePerson(UpdatedPerson);
                }
                else
                {
                    MessageBox.Show("Update failed");

                }
            }

             
            
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            ChangePhoto();
        }

        private void LLsetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.gif;*.bmp;*.png"; 
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                string SelectedPath = openFileDialog1.FileName;
                pbPersonImage.Load(SelectedPath);
                llRemoveImage.Visible = true;
                _IsCustomPic = true;
            }

        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;
            _IsCustomPic = false;
            if (rbMale.Checked == true)
            {
                pbPersonImage.Image = Properties.Resources.MaleUser;
            }
            if (rbFemale.Checked == true)
            {
                pbPersonImage.Image = Properties.Resources.FemalUser;

            }
            llRemoveImage.Visible = false;
        }
    }
}
