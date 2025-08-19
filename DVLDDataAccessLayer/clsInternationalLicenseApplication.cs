using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsInternationalLicenseApplication
    {

        public static int AddNewLocalDrivingLicenseApplications(int ApplicationID, int DriverID,int IssuedUsingLocalLicenseID
                            ,DateTime IssueDate,DateTime ExpirationDate,bool IsActive,int CreatedByUserID)
        {
            int insertedID = -1;

            string query = @"INSERT INTO [dbo].[InternationalLicenses] ([ApplicationID], [DriverID],[IssuedUsingLocalLicenseID],
                                [IssueDate], [ExpirationDate],[IsActive],[CreatedByUserID])
                             VALUES (@ApplicationID, @DriverID,@IssuedUsingLocalLicenseID,
                                @IssueDate ,@ExpirationDate,@IsActive,@CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                command.Parameters.AddWithValue("@IsActive", IsActive);
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

        public static bool IsLocalDrivingLicenseApplicationsExist(int DriverID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT 1 FROM InternationalLicenses WHERE DriverID = @DriverID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", DriverID);

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
            }

            return isFound;
        }



        public static DataTable GetAllinternationalLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT InternationalLicenses.InternationalLicenseID,\r\nInternationalLicenses.ApplicationID,\r\nInternationalLicenses.DriverID,\r\nInternationalLicenses.IssueDate, \r\nInternationalLicenses.ExpirationDate,InternationalLicenses.IsActive,\r\nLicenses.LicenseID\r\nFROM InternationalLicenses\r\ninner join Licenses on Licenses.LicenseID=InternationalLicenses.IssuedUsingLocalLicenseID\r\n ORDER BY InternationalLicenseID Desc";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        dt.Load(reader);
                        reader.Close();
                    }
                    catch
                    {
                        // Gérer l’erreur si nécessaire
                    }
                }
            }

            return dt;
        }


    }
}
