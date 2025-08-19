using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDDataAccessLayer
{
    public class ClsDetaindLicense
    {

        public static bool IsLicenseDetained(int LicenseID)
        {
        
            bool isFound = false;

            string query = @"select * from DetainedLicenses where LicenseID=@LicenseID and IsReleased=0 ";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {

                command.Parameters.AddWithValue("@LicenseID", LicenseID);

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

        public static int InsertDetainedLicense( int licenseID, DateTime DetainDate, float fineFees, int createdByUserID, bool isReleased, DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {
            int insertedID = -1;
            string query = @"INSERT INTO DetainedLicenses (LicenseID,DetainDate, FineFees, CreatedByUserID,IsReleased,ReleaseDate,ReleasedByUserID,ReleaseApplicationID) 
                             VALUES (@LicenseID,@DetainDate, @FineFees, @CreatedByUserID,@IsReleased,@ReleaseDate,@ReleasedByUserID,@ReleaseApplicationID); SELECT SCOPE_IDENTITY()";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LicenseID", licenseID);
                command.Parameters.AddWithValue("@DetainDate", DetainDate);
                command.Parameters.AddWithValue("@FineFees", fineFees);
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
                command.Parameters.AddWithValue("@IsReleased", isReleased);
                command.Parameters.AddWithValue("@ReleaseDate",releaseDate == DetainDate ? DBNull.Value : (object)releaseDate);

                command.Parameters.AddWithValue("@ReleasedByUserID",releasedByUserID == -1 ? DBNull.Value : (object)releasedByUserID);

                command.Parameters.AddWithValue("@ReleaseApplicationID",releaseApplicationID == -1 ? DBNull.Value : (object)releaseApplicationID);


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


        public static bool ReleaseDetainedLicense(int detainID, DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {
            bool isUpdated = false;
            string query = @"UPDATE DetainedLicenses SET IsReleased=1, ReleaseDate=@ReleaseDate, ReleasedByUserID=@ReleasedByUserID, ReleaseApplicationID=@ReleaseApplicationID WHERE DetainID=@DetainID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DetainID", detainID);
                command.Parameters.AddWithValue("@ReleaseDate", releaseDate);
                command.Parameters.AddWithValue("@ReleasedByUserID", releasedByUserID);
                command.Parameters.AddWithValue("@ReleaseApplicationID", releaseApplicationID);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    isUpdated = rowsAffected > 0;
                }
                catch
                {
                    isUpdated = false;
                }
            }
            return isUpdated;
        }

        public static bool GetDetainedLicenseInfo(ref int detainID, int licenseID,ref DateTime DetainDate,ref float fineFess,ref int createdByUserID, ref bool isReleased,ref DateTime releaseDate,ref int releasedByUserID,ref int releasedApplicationID)
        {
            bool isFound = false;

            string query = "select  top 1 * from DetainedLicenses where LicenseID=@LicenseID order by DetainDate desc";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LicenseID", licenseID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            detainID = Convert.ToInt32(reader["DetainID"]);
                            licenseID = Convert.ToInt32(reader["LicenseID"]);
                            DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                            fineFess = Convert.ToSingle(reader["FineFees"]);
                            createdByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                            isReleased = Convert.ToBoolean(reader["IsReleased"]);
                            releaseDate = reader["ReleaseDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReleaseDate"]) : DateTime.MinValue;
                            releasedByUserID = reader["ReleasedByUserID"] != DBNull.Value ? Convert.ToInt32(reader["ReleasedByUserID"]) : -1;
                            releasedApplicationID = reader["ReleaseApplicationID"] != DBNull.Value ? Convert.ToInt32(reader["ReleaseApplicationID"]) : -1;

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération de l'application : " + ex.Message);
                    isFound = false;
                }
            }

            return isFound;

        }


        public static DataTable GetAll()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "\r\nSELECT DetainedLicenses.DetainID, DetainedLicenses.LicenseID,DetainedLicenses.DetainDate, DetainedLicenses.IsReleased, DetainedLicenses.FineFees, DetainedLicenses.ReleaseDate, People.NationalNo, concat(People.FirstName,' ', People.SecondName,' ', People.ThirdName,' ' ,People.LastName)As FullName, \r\n             DetainedLicenses.ReleaseApplicationID\r\nFROM   Licenses INNER JOIN\r\n             Drivers ON Licenses.DriverID = Drivers.DriverID INNER JOIN\r\n             People ON Drivers.PersonID = People.PersonID INNER JOIN\r\n             DetainedLicenses ON Licenses.LicenseID = DetainedLicenses.LicenseID  \t\t\t order by DetainID desc\r\n";

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

    }
}
