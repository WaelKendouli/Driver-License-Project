using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountriesDataAccess
{
    public class clsCountriesDataAccess
    {
        public static string ConnectionString = "Server=.;Database=DVLD;Integrated Security=True;";

        public static DataTable GetAllCountries()
        {
            DataTable Countries = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT * FROM Countries";
            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    Countries.Load(reader);
                    reader.Close();
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return Countries;
        }

        public static bool FindCountryByID(int CountryID, ref string CountryName)
        {
            bool isFound = false;
            SqlConnection Connetion = new SqlConnection(ConnectionString);

            string Query = @"SELECT * FROM Countries WHERE CountryID = @CountryID";
            SqlCommand command = new SqlCommand(Query, Connetion);

            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                Connetion.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    CountryID = Convert.ToInt32(reader["CountryID"]);
                    CountryName = (string)reader["CountryName"];
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                Connetion.Close();
            }
            return isFound;
        }


        public static bool FindCountryByName(int CountryID, ref string CountryName)
        {
            bool isFound = false;
            SqlConnection Connetion = new SqlConnection(ConnectionString);

            string Query = @"SELECT * FROM Countries WHERE CountryName = @CountryName";
            SqlCommand command = new SqlCommand(Query, Connetion);

            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                Connetion.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    CountryID = Convert.ToInt32(reader["CountryID"]);
                    CountryName = (string)reader["CountryName"];
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                Connetion.Close();
            }
            return isFound;
        }

    }
}
