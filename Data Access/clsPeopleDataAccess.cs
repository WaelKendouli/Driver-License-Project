using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleBussinessLayer;
using System.Net;
using System.Security.Policy;

namespace PeopleDataAccessLayer
{
    public static class clsPeopleDataAccess
    {
        private static string ConnectionString = "Server=.;Database=DVLD;Integrated Security=True;";

        public static bool AddNewPeople( ref clsPerson person)
        {
           bool isAdded = false;
            SqlConnection Connection = new SqlConnection(ConnectionString);
            string query = @"INSERT INTO [dbo].[People]
           ([NationalNo]
           ,[FirstName]
           ,[SecondName]
           ,[ThirdName]
           ,[LastName]
           ,[DateOfBirth]
           ,[Gendor]
           ,[Address]
           ,[Phone]
           ,[Email]
           ,[NationalityCountryID]
           ,[ImagePath])
     VALUES
           (@NationalNo
           ,@FirstName
           ,@SecondName
           ,@ThirdName
           ,@LastName
           ,@DateOfBirth
           ,@Gendor
           ,@Address
           ,@Phone
           ,@Email
           ,@NationalityCountryID
           ,@ImagePath);SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@NationalNo" , person.NationalNo );
            Command.Parameters.AddWithValue("@FirstName", person.FirstName);
            Command.Parameters.AddWithValue("@LastName", person.LastName);
           Command.Parameters.AddWithValue("@SecondName", person.SecondName);

            if (person.ThirdName == "")
            {
                Command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            }
            else
            {
                Command.Parameters.AddWithValue("@ThirdName", person.ThirdName);

            }
            Command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);
            Command.Parameters.AddWithValue("@Gendor", person.Gendor);
            Command.Parameters.AddWithValue("@Address", person.Address);
            Command.Parameters.AddWithValue("@Phone", person.Phone);
            Command.Parameters.AddWithValue("@Email", person.Email);
            Command.Parameters.AddWithValue("@NationalityCountryID", person.NationalityCountryID);
            if (person.ImagePath == "")
            {
                Command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            }
            else
            {
                Command.Parameters.AddWithValue("@ImagePath", person.ImagePath);

            }


            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    isAdded = true;
                }
                else
                {
                    isAdded = false;
                }

            }
            catch (Exception ex)
            {
                isAdded = false;
            }
            finally
            {
                Connection.Close();

            }
            return isAdded;
        }

        public static bool FindPerson(ref int PersonID ,
            ref string NationalNo , ref string FirstName , ref string SecondName
            , ref string ThirdName
            , ref string LastName , ref DateTime DateOfBirth , ref int Gendor
            , ref string Address , ref string Phone , ref string Email
            , ref int NationalityCountryID , ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "SELECT * FROM People WHERE PersonID = @PersonID";
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
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    SecondName = (string)reader["SecondName"];
                    
                    if (reader["ThirdName"] == DBNull.Value)
                    {
                        ThirdName = "";
                    }
                    else
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gendor = Convert.ToInt32( reader["Gendor"]);
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
           
                    if (reader["Email"] == DBNull.Value)
                    {
                        Email = "";
                    }
                    else
                    {
                        Email = (string)reader["Email"];
                    }
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] == DBNull.Value)
                    {
                        ImagePath = "";
                    }
                    else
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }


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


        public static bool FindPersonByNationalNumber(ref int PersonID,
            ref string NationalNo, ref string FirstName, ref string SecondName
            , ref string ThirdName
            , ref string LastName, ref DateTime DateOfBirth, ref int Gendor
            , ref string Address, ref string Phone, ref string Email
            , ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "SELECT * FROM People WHERE NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);



            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    SecondName = (string)reader["SecondName"];

                    if (reader["ThirdName"] == DBNull.Value)
                    {
                        ThirdName = "";
                    }
                    else
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gendor = Convert.ToInt32(reader["Gendor"]);
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];

                    if (reader["Email"] == DBNull.Value)
                    {
                        Email = "";
                    }
                    else
                    {
                        Email = (string)reader["Email"];
                    }
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] == DBNull.Value)
                    {
                        ImagePath = "";
                    }
                    else
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }


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

        public static DataTable FilterContentBy(string FilteringMethod ,string Filter)
        {
            DataTable People = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = $@"Select PersonID ,NationalNo,FirstName,
SecondName,ThirdName,LastName,DateOfBirth,GendorTitle = 
case 
when Gendor = 0 then 'Male'
when Gendor = 1 then 'Female'
end
, Address ,Phone , Email , CountryName ,ImagePath
from People inner join Countries on People.NationalityCountryID = Countries.CountryID
WHERE [{FilteringMethod}] = @Filter";
            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@FilteringMethod", FilteringMethod);
            Command.Parameters.AddWithValue("@Filter", Filter);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    People.Load(reader);
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
            return People;
        }

        public static DataTable ShowContent()
        {
            DataTable People = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"Select PersonID ,NationalNo,FirstName,
SecondName,ThirdName,LastName,DateOfBirth,GendorTitle = 
case 
when Gendor = 0 then 'Male'
when Gendor = 1 then 'Female'
end
, Address ,Phone , Email , CountryName ,ImagePath
from People inner join Countries on People.NationalityCountryID = Countries.CountryID";
            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    People.Load(reader);
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
            return People;
        }


        public static bool UpdatePerson(int PersonID , ref clsPerson Person)
        {
            bool isUpdated = false; 
            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = @"UPDATE [dbo].[People]
   SET [NationalNo] = @NationalNo
      ,[FirstName] = @FirstName
      ,[SecondName] = @SecondName
      ,[ThirdName] = @ThirdName
      ,[LastName] = @LastName
      ,[DateOfBirth] = @DateOfBirth
      ,[Gendor] = @Gendor
      ,[Address] = @Address
      ,[Phone] = @Phone
      ,[Email] = @Email
      ,[NationalityCountryID] = @NationalityCountryID
      ,[ImagePath] = @ImagePath
 WHERE PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@NationalNo", Person.NationalNo);
            Command.Parameters.AddWithValue("@FirstName", Person.FirstName);
            Command.Parameters.AddWithValue("@SecondName", Person.SecondName);
            Command.Parameters.AddWithValue("@ThirdName", Person.ThirdName);
            Command.Parameters.AddWithValue("@LastName", Person.LastName);
            Command.Parameters.AddWithValue("@DateOfBirth", Person.DateOfBirth);
            Command.Parameters.AddWithValue("@Gendor", Person.Gendor);
            Command.Parameters.AddWithValue("@Address", Person.Address);
            Command.Parameters.AddWithValue("@Phone", Person.Phone);
            Command.Parameters.AddWithValue("@Email", Person.Email);
            Command.Parameters.AddWithValue("@NationalityCountryID", Person.NationalityCountryID);
            Command.Parameters.AddWithValue("@ImagePath", Person.ImagePath);

            try
            {
                Connection.Open();
                int RowsEffected = Command.ExecuteNonQuery();

                if (RowsEffected>0)
                {
                    isUpdated = true;
                }
                else
                {
                    isUpdated = false;
                }


            }
            catch(Exception ex)
            {
                isUpdated = false;
            }
            finally
            {
                Connection.Close();
            }
            return isUpdated;
        }


        public static bool DeleteRecord(int PersonID)
        {
            bool isDeleted = false;
            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = @"DELETE FROM People Where PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID",PersonID);

            try
            {
                Connection.Open();
                int rowsEffected = Command.ExecuteNonQuery();
                if (rowsEffected>0)
                {
                    isDeleted = true;
                }

            }
            catch (Exception ex)
            {
                isDeleted = false;
            }
            finally
            {
                Connection.Close();
            }
            return isDeleted;
        }

        public static bool IsPersonExist(string NationalNo) // Searching is done by national number 
        {
           bool isExsist = false;
            SqlConnection connection = new SqlConnection(ConnectionString);
 
            string Query = @"Select x=4 FROM People WHERE NationalNo = @NationalNo ";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isExsist = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isExsist = false;
            }
            finally
            {
                connection.Close(); 
            }
            return isExsist;
        }



        public static clsPerson GetSpecificPersonByFilter(string FilteringMethod, string Filter)
        {
            clsPerson Person = new clsPerson();
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = $"SELECT * FROM People WHERE [{FilteringMethod}] = @Filter";
            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@FilteringMethod", FilteringMethod);
            Command.Parameters.AddWithValue("@Filter", Filter);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Person.PersonID = Convert.ToInt32(reader["PersonID"]);
                    Person.NationalNo = (string)reader["NationalNo"];
                    Person.FirstName = (string)reader["FirstName"];
                    Person.LastName = (string)reader["LastName"];
                    Person.SecondName = (string)reader["SecondName"];

                    if (reader["ThirdName"] == DBNull.Value)
                    {
                        Person.ThirdName = "";
                    }
                    else
                    {
                        Person.ThirdName = (string)reader["ThirdName"];
                    }
                    Person.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Person.Gendor = Convert.ToInt32(reader["Gendor"]);
                    Person.Address = (string)reader["Address"];
                    Person.Phone = (string)reader["Phone"];

                    if (reader["Email"] == DBNull.Value)
                    {
                        Person.Email = "";
                    }
                    else
                    {
                        Person.Email = (string)reader["Email"];
                    }
                    Person.NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] == DBNull.Value)
                    {
                        Person.ImagePath = "";
                    }
                    else
                    {
                        Person.ImagePath = (string)reader["ImagePath"];
                    }


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
            return Person; 
        }


    }

   
}
