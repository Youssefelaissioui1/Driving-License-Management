using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsTestAppointments
    {


        public static DataTable GetAllTestOfVision(int LocalDrivingLicenseApplicationID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from TestAppointments where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestTypeID=@TestTypeID order by TestAppointmentID Desc ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
             int visionTestTypeID = 1; // ou la valeur correcte correspondant au test vision
            command.Parameters.AddWithValue("@TestTypeID", visionTestTypeID);

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
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }




        public static DataTable GetAllTestOfWriting(int LocalDrivingLicenseApplicationID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from TestAppointments where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestTypeID=@TestTypeID order by TestAppointmentID Desc ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            int visionTestTypeID = 2; // ou la valeur correcte correspondant au test vision
            command.Parameters.AddWithValue("@TestTypeID", visionTestTypeID);

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
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }



        public static DataTable GetAllTestOfStreet(int LocalDrivingLicenseApplicationID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from TestAppointments where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestTypeID=@TestTypeID order by TestAppointmentID Desc ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            int visionTestTypeID = 3; // ou la valeur correcte correspondant au test vision
            command.Parameters.AddWithValue("@TestTypeID", visionTestTypeID);

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
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }


        public static int AddTestAppointmentAndGetID(
            int testTypeID,
            int localDrivingLicenseApplicationID,
            DateTime appointmentDate,
            decimal paidFees,
            int createdByUserID,
            bool isLocked)
        {
            int newID = -1;

            string connectionString = clsDataAccessSettings.ConnectionString;

            string query = @"INSERT INTO [dbo].[TestAppointments]
                    ([TestTypeID],
                     [LocalDrivingLicenseApplicationID],
                     [AppointmentDate],
                     [PaidFees],
                     [CreatedByUserID],
                     [IsLocked])
                     VALUES
                     (@TestTypeID,
                      @LocalDrivingLicenseApplicationID,
                      @AppointmentDate,
                      @PaidFees,
                      @CreatedByUserID,
                      @IsLocked);

                     SELECT SCOPE_IDENTITY();"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TestTypeID", testTypeID);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
                command.Parameters.AddWithValue("@PaidFees", paidFees);
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
                command.Parameters.AddWithValue("@IsLocked", isLocked);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar(); // ✅ lit la première valeur retournée
                    if (result != null)
                    {
                        newID = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de l’insertion : " + ex.Message);
                }
            }

            return newID;
        }


        public static DataRow GetInfoTestID(int LocalDrivingLicenseApplicationID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT TOP 1 * \r\nFROM TestAppointments \r\n" +
                    "WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID \r\nORDER BY AppointmentDate DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                        return dt.Rows[0];
                    else
                        return null;
                }
            }
        }



        public static bool GetInfoTestID2(
    int LocalDrivingLicenseApplicationID,
    ref int TestAppointmentID,
    ref int testTypeID,
    ref DateTime appointmentDate,
    ref decimal paidFees,
    ref int createdByUserID,
    ref bool isLocked)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT TOP 1 * FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID ORDER BY TestAppointmentID DESC";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        TestAppointmentID = (int)reader["TestAppointmentID"];
                        testTypeID = (int)reader["testTypeID"];
                        appointmentDate = (DateTime)reader["AppointmentDate"];
                        paidFees = (decimal)reader["PaidFees"];
                        createdByUserID = (int)reader["CreatedByUserID"];
                        isLocked = (bool)reader["IsLocked"];
                    }
                    else
                    {
                        isFound = false;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Log exception si besoin
                    isFound = false;
                }
            }

            return isFound;
        }



        public static bool isExist(int LocalDrivingLicenseApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and IsLocked=0";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

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


        public static bool UpdateTestAppointments(
    int testAppointmentID,bool IsLocked,
    DateTime appointmentDate)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE TestAppointments
                         SET  IsLocked =@IsLocked ,
                        appointmentDate=@appointmentDate
                         WHERE TestAppointmentID = @testAppointmentID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@testAppointmentID", SqlDbType.Int).Value = testAppointmentID;
                command.Parameters.Add("@IsLocked", SqlDbType.Bit).Value = IsLocked;
                command.Parameters.Add("@appointmentDate", SqlDbType.DateTime).Value = appointmentDate;


                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Console.WriteLine("Erreur : " + ex.Message);
                    return false;
                }
            }

            return rowsAffected > 0;
        }




        public static bool GetInfoTestIDtest(
    ref int LocalDrivingLicenseApplicationID,
     int TestAppointmentID,
    ref int testTypeID,
    ref DateTime appointmentDate,
    ref decimal paidFees,
    ref int createdByUserID,
    ref bool isLocked)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT TOP 1 * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID ORDER BY AppointmentDate DESC";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        TestAppointmentID = (int)reader["TestAppointmentID"];
                        testTypeID = (int)reader["testTypeID"];
                        appointmentDate = (DateTime)reader["AppointmentDate"];
                        paidFees = (decimal)reader["PaidFees"];
                        createdByUserID = (int)reader["CreatedByUserID"];
                        isLocked = (bool)reader["IsLocked"];
                    }
                    else
                    {
                        isFound = false;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Log exception si besoin
                    isFound = false;
                }
            }

            return isFound;
        }


        public static int GetAllTestOfVision_Count(int LocalDrivingLicenseApplicationID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from TestAppointments where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestTypeID=@TestTypeID order by TestAppointmentID Desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            int writingTestTypeID = 1;
            command.Parameters.AddWithValue("@TestTypeID", writingTestTypeID);

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
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            // Retourne le nombre de lignes
            return dt.Rows.Count;
        }



        public static int GetAllTestOfWriting_Count(int LocalDrivingLicenseApplicationID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from TestAppointments where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestTypeID=@TestTypeID order by TestAppointmentID Desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            int writingTestTypeID = 2; 
            command.Parameters.AddWithValue("@TestTypeID", writingTestTypeID);

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
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            // Retourne le nombre de lignes
            return dt.Rows.Count;
        }

        public static int GetAllTestOfStreet_Count(int LocalDrivingLicenseApplicationID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from TestAppointments where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestTypeID=@TestTypeID order by TestAppointmentID Desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            int writingTestTypeID = 3;
            command.Parameters.AddWithValue("@TestTypeID", writingTestTypeID);

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
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            // Retourne le nombre de lignes
            return dt.Rows.Count;
        }



    }
}
