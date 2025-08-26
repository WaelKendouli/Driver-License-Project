using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountriesBussinessLayer;
using PeopleDataAccessLayer;
namespace PeopleBussinessLayer
{
    public class clsPerson
    {
     
        private enum eModes
        {
            eAddNew, eUpdate
        }
        private eModes _Mode = eModes.eAddNew;
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gendor { get; set; }
        public string Address { get; set; }
        public clsCountry Country;
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }
        public string FullName {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }
        public clsPerson()
        {
            _Mode = eModes.eAddNew;
            PersonID = -1;
            NationalNo = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth = DateTime.MinValue;
            Gendor = 0;
           
            Address = "";
            Phone = "";
            Email = "";
            NationalityCountryID = 0;
            ImagePath = "";

        }

        private clsPerson(int PersonID,string NationalNo, string FirstName,
            string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, int Gendor, string Address
            , string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            _Mode = eModes.eUpdate;
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.ThirdName = ThirdName;
            this.SecondName = SecondName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;

        }

        public static clsPerson FindPersonByID(int PersonID)
        {
            string NationalNo = ""; string FirstName = "";
            string SecondName = ""; string ThirdName = ""; string LastName = "";
            DateTime DateOfBirth = DateTime.Now; int Gendor = 0; string Address = "";
            string Phone = ""; string Email = ""; int NationalityCountryID = 0; string ImagePath = "";

            if (clsPeopleDataAccess.FindPerson(ref PersonID, ref NationalNo, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName,
                ref DateOfBirth, ref Gendor, ref Address,
                ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID,NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            return null;
        }
        public static bool DeletePerson(int PersonID)
        {
            if (clsPeopleDataAccess.DeleteRecord(PersonID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool _UpdatePerson(int PersonID,  clsPerson Person)
        {
            if (clsPeopleDataAccess.UpdatePerson(PersonID, ref Person))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool _AddNewPerson(clsPerson Person)
        {
            if (clsPeopleDataAccess.AddNewPeople(ref Person))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static DataTable GetAllPeople()
        {
            return clsPeopleDataAccess.ShowContent();
        }

        public static DataTable GetListPeopleFilteredBy(string FilteringMethod , string Filter)
        {
            return clsPeopleDataAccess.FilterContentBy(FilteringMethod , Filter);
        }

        public static DataTable GetAllCountries()
        {
            return clsCountry.GetAllCountries();
        }

        public static bool isPersonExist(string NationalNo)
        {
            return clsPeopleDataAccess.IsPersonExist(NationalNo);
        }

        public static clsPerson GetSpecificPersonByFilter(string FilteringMethod, string Filter)
        {
            clsPerson FoundPerson = clsPeopleDataAccess.GetSpecificPersonByFilter(FilteringMethod, Filter);
            if (FoundPerson!=null)
            {
                FoundPerson._Mode = eModes.eUpdate;
            return FoundPerson; 
            }
            else
            {
                return null;
            }

        }
        public static clsPerson FindPersonByNationalNo(string NationalNo)
        {
            int PersonID = -1; string FirstName = "";
            string SecondName = ""; string ThirdName = ""; string LastName = "";
            DateTime DateOfBirth = DateTime.Now; int Gendor = 0; string Address = "";
            string Phone = ""; string Email = ""; int NationalityCountryID = 0; string ImagePath = "";

            if (clsPeopleDataAccess.FindPersonByNationalNumber(ref PersonID, ref NationalNo, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName,
                ref DateOfBirth, ref Gendor, ref Address,
                ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            return null;
        }

        public static int GetPersonID(string NationalNo )
        {
            clsPerson Person = FindPersonByNationalNo(NationalNo);
            if (Person!=null)
            {
                return Person.PersonID;
            }
            else
            {
                return 0;
            }
        }

        public bool Save()
        {
            if (_Mode==eModes.eUpdate)
            {
                return _UpdatePerson(this.PersonID, this);
            }
            else
            {
                if (clsPeopleDataAccess.IsPersonExist(this.NationalNo))
                {
                    return false; 
                }
                return _AddNewPerson(this);
            }
        }

    }
}
