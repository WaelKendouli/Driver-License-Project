using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionToMyDataBase;
namespace DriversDataAccess
{
    public class clsDriversDataAccess
    {
        public static int AddNewDriver(int PersonID, int CreatedByUserID,
        DateTime CreatedDate)
        {
            int DriverID = -1;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);
            string Query = @"INSERT INTO [dbo].[Drivers]
           ([PersonID]
           ,[CreatedByUserID]
           ,[CreatedDate])
     VALUES
           (@PersonID
           ,@CreatedByUserID
           ,@CreatedDate);SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DriverID = insertedID;
                }
                else
                {
                    DriverID = -1;
                }

            }
            catch (Exception ex)
            {
                DriverID = -1;

            }
            finally
            {
                Connection.Close();

            }
            return DriverID;

        }


        public static bool FindDriver(int DriverID ,ref int PersonID,ref int CreatedByUserID,
       ref DateTime CreatedDate)
        {
            
                bool isFound = false;
                SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

                string Query = @"SELECT * FROM Drivers
Where Drivers.DriverID = @DriverID";
                SqlCommand command = new SqlCommand(Query, connection);

                command.Parameters.AddWithValue("@DriverID", DriverID);



                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;
                        PersonID = Convert.ToInt32(reader["PersonID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                        


                        reader.Close();
                    }
                    else
                    {
                        isFound = false;

                    }

                }
                catch (Exception ex)
                {
                    isFound = false;
                }
                finally
                {

                    connection.Close();
                }
                return isFound;
            
        }


        public static bool FindDriverByPersonID(ref int DriverID,  int PersonID, ref int CreatedByUserID,
       ref DateTime CreatedDate)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"SELECT * FROM Drivers
Where Drivers.PersonID = @PersonID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);



            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);



                    reader.Close();
                }
                else
                {
                    isFound = false;

                }

            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {

                connection.Close();
            }
            return isFound;

        }

        public static bool isPersonExistsAsDriver(int PersonID)
        {
            bool isExists = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"Select W=10 From Drivers 
Where Drivers.PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);



            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isExists = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isExists = false;
            }
            finally
            {
                connection.Close();
            }
            return isExists;
        }

        public static DataTable GetDriverList()
        {

            DataTable dtDrivers = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string query = @"Select * from Drivers_View";
            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    dtDrivers.Load(reader);
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
            return dtDrivers;

        }


    }
}
