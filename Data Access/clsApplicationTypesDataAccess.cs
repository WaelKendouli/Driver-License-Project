using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTypesDataAccess
{
    public class clsApplicationTypesDataAccess
    {
        public static string ConnectionString = "Server=.;Database=DVLD;Integrated Security=True;";

        public static DataTable GetAllApplicationTypes()
        {
            DataTable AppTypes = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"select * from ApplicationTypes";
            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    AppTypes.Load(reader);
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
            return AppTypes;
        }

        public static bool FindApplicationTypeByID(int ApplicationTypeID, ref string ApplicationTypeTitle, ref double ApplicationFees)
        {
            bool isFound = false;
            SqlConnection Connetion = new SqlConnection(ConnectionString);

            string Query = @"select * from ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";
            SqlCommand command = new SqlCommand(Query, Connetion);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            try
            {
                Connetion.Open();
                //ApplicationFees
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = Convert.ToDouble(reader["ApplicationFees"]);

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

        public static bool FindApplicationTypeByTitle(ref int ApplicationTypeID,  string ApplicationTypeTitle, ref double ApplicationFees)
        {
            bool isFound = false;
            SqlConnection Connetion = new SqlConnection(ConnectionString);

            string Query = @"select * from ApplicationTypes WHERE ApplicationTypeTitle = @ApplicationTypeTitle";
            SqlCommand command = new SqlCommand(Query, Connetion);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            try
            {
                Connetion.Open();
                //ApplicationFees
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = Convert.ToDouble(reader["ApplicationFees"]);

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

        public static bool UpdateApplicationType( int ApplicationTypeID ,  string ApplicationTypeTitle,  double ApplicationFees)
        {

            bool isUpdated = false;
            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = @"UPDATE [dbo].[ApplicationTypes]
   SET [ApplicationTypeTitle] = @ApplicationTypeTitle
      ,[ApplicationFees] = @ApplicationFees
 WHERE ApplicationTypeID = @ApplicationTypeID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            Command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            Command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            

            try
            {
                Connection.Open();
                int RowsEffected = Command.ExecuteNonQuery();

                if (RowsEffected > 0)
                {
                    isUpdated = true;
                }
                else
                {
                    isUpdated = false;
                }


            }
            catch (Exception ex)
            {
                isUpdated = false;
            }
            finally
            {
                Connection.Close();
            }
            return isUpdated;

        }

    }
}
