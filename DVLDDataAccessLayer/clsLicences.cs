using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDDataAccessLayer
{
    public class clsLicences
    {

        public static int AddNewLicence(
            int ApplicationID,
            int DriverID,
            int LicenseClass,
            DateTime IssueDate,
            DateTime ExpirationDate,
            string Notes,
            float PaidFees,
            bool IsActive,
            short IssueReason,
            int CreatedByUserID

            )
        {
            int insertedID = -1;

            string query = @"
INSERT INTO [dbo].[Licenses]
           ([ApplicationID]
           ,[DriverID]
           ,[LicenseClass]
           ,[IssueDate]
           ,[ExpirationDate]
           ,[Notes]
           ,[PaidFees]
           ,[IsActive]
           ,[IssueReason]
           ,[CreatedByUserID])
       VALUES
           (@ApplicationID
           ,@DriverID
           ,@LicenseClass
           ,@IssueDate
           ,@ExpirationDate
           ,@Notes
           ,@PaidFees
           ,@IsActive
           ,@IssueReason
           ,@CreatedByUserID);SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                string notesValue = Notes;
                command.Parameters.AddWithValue("@Notes", (object)notesValue ?? DBNull.Value);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@IssueReason", IssueReason);
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

        public static bool IsLicenseExist(int ApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Licenses WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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


        public static bool GetLicenseInfoByApplicationID(
    int applicationID,
    ref int licenseID,
    ref int driverID,
    ref int licenseClass,
    ref DateTime issueDate,
    ref DateTime expirationDate,
    ref string notes,
    ref float paidFees,
    ref bool isActive,
    ref short issueReason,
    ref int createdByUserID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
            SELECT TOP 1 LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID
            FROM Licenses
            WHERE ApplicationID = @ApplicationID
            ORDER BY LicenseID DESC"; // Optionnel : prendre la licence la plus récente

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", applicationID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        licenseID = (int)reader["LicenseID"];
                        driverID = (int)reader["DriverID"];
                        licenseClass = (int)reader["LicenseClass"];
                        issueDate = (DateTime)reader["IssueDate"];
                        expirationDate = (DateTime)reader["ExpirationDate"];
                        notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : (string)reader["Notes"];
                        paidFees = Convert.ToSingle((decimal)reader["PaidFees"]);
                        isActive = (bool)reader["IsActive"];
                        issueReason = (short)(byte)reader["IssueReason"];  // tinyint => byte => short
                        createdByUserID = (int)reader["CreatedByUserID"];

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




        public static DataTable GetAllLicense_Of_Person(int PersonID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "  select  Licenses.LicenseID,Licenses.LicenseClass,Licenses.ApplicationID,Licenses.IssueDate,Licenses.ExpirationDate, Licenses.IsActive  from Licenses   inner join Applications on Licenses.ApplicationID=Applications.ApplicationID\r\nwhere Applications.ApplicantPersonID=@PersonID order by Licenses.ApplicationID Desc ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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


        public static DataTable GetAllInternationalLicense(int driverID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select InternationalLicenses.InternationalLicenseID,InternationalLicenses.ApplicationID,Licenses.LicenseID ,InternationalLicenses.IssueDate,InternationalLicenses.ExpirationDate,InternationalLicenses.IsActive from InternationalLicenses inner join Licenses ON   licenses.LicenseID=InternationalLicenses.IssuedUsingLocalLicenseID  where InternationalLicenses.DriverID=@DriverID order by Licenses.LicenseID Desc \r\n";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@driverID", driverID);

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



        public static bool GetLicenseInfoByLicenseID(
    ref int ApplicationID,
     int licenseID,
    ref int driverID,
    ref int licenseClass,
    ref DateTime issueDate,
    ref DateTime expirationDate,
    ref string notes,
    ref float paidFees,
    ref bool isActive,
    ref short issueReason,
    ref int createdByUserID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
            SELECT TOP 1 LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID
            FROM Licenses
            WHERE licenseID = @licenseID
            ORDER BY LicenseID DESC";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@licenseID", licenseID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        ApplicationID = (int)reader["ApplicationID"];
                        driverID = (int)reader["DriverID"];
                        licenseClass = (int)reader["LicenseClass"];
                        issueDate = (DateTime)reader["IssueDate"];
                        expirationDate = (DateTime)reader["ExpirationDate"];
                        notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : (string)reader["Notes"];
                        paidFees = Convert.ToSingle((decimal)reader["PaidFees"]);
                        isActive = (bool)reader["IsActive"];
                        issueReason = (short)(byte)reader["IssueReason"];  // tinyint => byte => short
                        createdByUserID = (int)reader["CreatedByUserID"];

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



        public static bool UpdateIsActiveLIcenseToFalse(int LicenseID)
        {
            int rowsAffected = 0;

            string query = @"UPDATE [dbo].[Licenses]
                             SET IsActive = 0
                             WHERE LicenseID = @LicenseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LicenseID", LicenseID);



                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch
                {
                    return false;
                }
            }
            return rowsAffected > 0; 

        }


        public static bool UpdateIsDetainedLIcenseToTrue(int LicenseID)
        {
            int rowsAffected = 0;

            string query = @"UPDATE [dbo].[Licenses]
                             SET IsActive = 0
                             WHERE LicenseID = @LicenseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LicenseID", LicenseID);



                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch
                {
                    return false;
                }
            }
            return rowsAffected > 0;

        }
    }
}
