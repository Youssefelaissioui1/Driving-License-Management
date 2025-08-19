using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDDataAccessLayer
{
    public class clsLicenseClassesData
    { 

        public static bool GetLicenseClassesInfoByID(int LicenseClassID, ref string ClassName,ref string ClassDescription,ref int MinimumAllowedAge,ref int DefaultValidityLength,ref float ClassFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select *from LicenseClasses where LicenseClassID=@LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);




                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static bool GetLicenseClassInfoByName(ref int LicenseClassID,  string ClassName, ref string ClassDescription, ref int MinimumAllowedAge, ref int DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LicenseClasses WHERE ClassName = @ClassName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    LicenseClassID = (int)reader["LicenseClassID"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        //public static int AddNew(string CountryName)
        //{
        //    //this function will return the new contact id if succeeded and -1 if not.
        //    int CountryID = -1;

        //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        //    string query = @"INSERT INTO Countries (CountryName)
        //                     VALUES (@CountryName);
        //                     SELECT SCOPE_IDENTITY();";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    command.Parameters.AddWithValue("@CountryName", CountryName);



        //    try
        //    {
        //        connection.Open();

        //        object result = command.ExecuteScalar();


        //        if (result != null && int.TryParse(result.ToString(), out int insertedID))
        //        {
        //            CountryID = insertedID;
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        //Console.WriteLine("Error: " + ex.Message);

        //    }

        //    finally
        //    {
        //        connection.Close();
        //    }


        //    return CountryID;
        //}

        //public static bool UpdateCountry(int ID, string CountryName)
        //{

        //    int rowsAffected = 0;
        //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        //    string query = @"Update  Countries  
        //                    set CountryName=@CountryName
                                
        //                        where CountryID = @CountryID";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    command.Parameters.AddWithValue("@CountryID", ID);
        //    command.Parameters.AddWithValue("@CountryName", CountryName);


        //    try
        //    {
        //        connection.Open();
        //        rowsAffected = command.ExecuteNonQuery();

        //    }
        //    catch (Exception ex)
        //    {
        //        //Console.WriteLine("Error: " + ex.Message);
        //        return false;
        //    }

        //    finally
        //    {
        //        connection.Close();
        //    }

        //    return (rowsAffected > 0);
        //}

        public static DataTable GetAllLicenseClasses()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT LicenseClassID, ClassName FROM LicenseClasses ORDER BY ClassName";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        //public static bool DeleteCountry(int CountryID)
        //{

        //    int rowsAffected = 0;

        //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        //    string query = @"Delete Countries 
        //                        where CountryID = @CountryID";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    command.Parameters.AddWithValue("@CountryID", CountryID);

        //    try
        //    {
        //        connection.Open();

        //        rowsAffected = command.ExecuteNonQuery();

        //    }
        //    catch (Exception ex)
        //    {
        //        // Console.WriteLine("Error: " + ex.Message);
        //    }
        //    finally
        //    {

        //        connection.Close();

        //    }

        //    return (rowsAffected > 0);

        //}

        //public static bool IsCountryExist(int ID)
        //{
        //    bool isFound = false;

        //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        //    string query = "SELECT Found=1 FROM Countries WHERE CountryID = @CountryID";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    command.Parameters.AddWithValue("@CountryID", ID);

        //    try
        //    {
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();

        //        isFound = reader.HasRows;

        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        //Console.WriteLine("Error: " + ex.Message);
        //        isFound = false;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }

        //    return isFound;
        //}


        //public static bool IsCountryExist(string CountryName)
        //{
        //    bool isFound = false;

        //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        //    string query = "SELECT Found=1 FROM Countries WHERE CountryName = @CountryName";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    command.Parameters.AddWithValue("@CountryName", CountryName);

        //    try
        //    {
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();

        //        isFound = reader.HasRows;

        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        //Console.WriteLine("Error: " + ex.Message);
        //        isFound = false;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }

        //    return isFound;
        //}


    }
}
