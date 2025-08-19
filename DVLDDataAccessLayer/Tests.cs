using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsTests
    {
        public static int AddNewTest(
      
          int TestAppointmentID ,
          bool TestResult,
          string Notes,
          int CreatedByUserID
          )
        {
            int insertedID = -1;

            string query = @"INSERT INTO[dbo].[Tests]
            ([TestAppointmentID]
                       , [TestResult]
                       , [Notes]
                       , [CreatedByUserID])
     VALUES
           (@TestAppointmentID
           ,@TestResult
           ,@Notes
           ,@CreatedByUserID);SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TestResult", TestResult);
                command.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            


                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        insertedID = id;
                    }
                }
                catch
                {
                    insertedID = -1;
                }
            }

            return insertedID;
        }


        public static bool GetTestsInfoByID(
         ref int TestID,
           int TestAppointmentID,
          ref bool TestResult,
          ref string Notes,
          ref int CreatedByUserID)
         
       
        {
            bool isFound = false;

            string query = " SELECT TOP 1 * FROM Tests WHERE TestAppointmentID = @TestAppointmentID ORDER BY TestID DESC;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            TestID = Convert.ToInt32(reader["TestID"]);
                            TestResult = Convert.ToBoolean(reader["TestResult"]);
                           Notes = Convert.ToString(reader["Notes"]);
                           CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the error (you can log to a file or system log)
                    Console.WriteLine("Erreur lors de la récupération de l'application : " + ex.Message);
                    isFound = false;
                }
            }

            return isFound;
        }



        //Vérifie si une application existe par person et classe
        public static bool IsTestExistID(int TestAppointmentID)
        {
            bool isFound = false;

            string query = @"SELECT 1 FROM Applications 
                             WHERE TestAppointmentID=@TestAppointmentID ORDER BY TestID DESC;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {

                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    isFound = reader.HasRows;
                    reader.Close();
                }
                catch
                {
                    isFound = false;
                }
            }

            return isFound;
        }


        public static bool IsTestExistIDAndIsPassofVision(int TestAppointmentID)
        {
            bool isFound = false;

            string query = @"
  SELECT TOP 1 *
FROM Tests
INNER JOIN TestAppointments ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
WHERE Tests.TestResult = 1 
  AND TestAppointments.TestTypeID = 1
  AND Tests.TestAppointmentID = @TestAppointmentID
ORDER BY Tests.TestID DESC;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {

                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    isFound = reader.HasRows;
                    reader.Close();
                }
                catch
                {
                    isFound = false;
                }
            }

            return isFound;
        }
    


    
        public static bool IsTestExistIDAndIsPassofWriting(int TestAppointmentID)
        {
            bool isFound = false;
            string query = @"
  SELECT TOP 1 *
FROM Tests
INNER JOIN TestAppointments ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
WHERE Tests.TestResult = 1 
  AND TestAppointments.TestTypeID = 2 
  AND Tests.TestAppointmentID = @TestAppointmentID
ORDER BY Tests.TestID DESC;";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {

                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    isFound = reader.HasRows;
                    reader.Close();
                }
                catch
                {
                    isFound = false;
                }
            }

            return isFound;
        }


        public static bool IsTestExistIDAndIsPassofStreet(int TestAppointmentID)
        {
            bool isFound = false;
            string query = @"
  SELECT TOP 1 *
FROM Tests
INNER JOIN TestAppointments ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
WHERE Tests.TestResult = 1 
  AND TestAppointments.TestTypeID = 3
  AND Tests.TestAppointmentID = @TestAppointmentID
ORDER BY Tests.TestID DESC;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {

                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    isFound = reader.HasRows;
                    reader.Close();
                }
                catch
                {
                    isFound = false;
                }
            }

            return isFound;
        }
    }
}
