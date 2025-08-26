using CountriesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountriesBussinessLayer
{
    public class clsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        private clsCountry(int CountryID , string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }
        public static DataTable GetAllCountries()
        {
            return clsCountriesDataAccess.GetAllCountries();
        }
        public static clsCountry Find(int CountryID)
        {
            string CountryName = "";
            if (clsCountriesDataAccess.FindCountryByID(CountryID ,ref CountryName))
            {
                return new clsCountry(CountryID , CountryName);
            }
            else
            {
                return null;
            }
        }
        public static clsCountry Find(string CountryName)
        {
            int CountryID = 0;
            if (clsCountriesDataAccess.FindCountryByName(CountryID, ref CountryName))
            {
                return new clsCountry(CountryID, CountryName);
            }
            else
            {
                return null;
            }
        }
        public static string FindCountryName(int CountryID)
        {
            string CountryName = "";
            if (clsCountriesDataAccess.FindCountryByID(CountryID, ref CountryName))
            {
                return CountryName;
            }
            else
            {
                return "";
            }
        }
    }
}
