using System;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDDataAccessLayer
{
    public class clsLocalDrivingLicenseApplications
    {
        // Obtenir les informations par ID
        public static bool GetLocalDrivingLicenseApplicationsInfoByID(
            int LocalDrivingLicenseApplicationID,
            ref string ClassName,
            ref string NationalNo,
            ref string FullName,
            ref DateTime ApplicationDate,
            ref float PassedTestCount,
            ref string Status)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM LocalDrivingLicenseApplications_View WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;
                        ClassName = (string)reader["ClassName"];
                        NationalNo = (string)reader["NationalNo"];
                        FullName = (string)reader["FullName"];
                        ApplicationDate = (DateTime)reader["ApplicationDate"];
                        PassedTestCount = Convert.ToSingle(reader["PassedTestCount"]);
                        Status = (string)reader["Status"];
                    }

                    reader.Close();
                }
                catch
                {
                    isFound = false;
                }
            }

            return isFound;
        }


        public static bool GetLocalDrivingLicenseApplicationsInfoByIDnotView(
           int LocalDrivingLicenseApplicationID,
           ref int ApplicationID,
           ref int LicenseClassID
          )
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;
                        ApplicationID = (int)reader["ApplicationID"];
                        LicenseClassID = (int)reader["LicenseClassID"];

                    }

                    reader.Close();
                }
                catch
                {
                    isFound = false;
                }
            }

            return isFound;
        }


        // Obtenir les informations par numéro national
        public static bool GetLocalDrivingLicenseApplicationsInfoByNationalNo_ClassName(
            ref int LocalDrivingLicenseApplicationID,
             string ClassName,
            string NationalNo,
            ref string FullName,
            ref DateTime ApplicationDate,
            ref float PassedTestCount,
            ref string Status)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM LocalDrivingLicenseApplications_View WHERE NationalNo like @NationalNo  and ClassName like @ClassName and Status like 'New';\r\n";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NationalNo", $"%{NationalNo}%");
                command.Parameters.AddWithValue("@ClassName", $"%{ClassName}%");


                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;
                        LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                        ClassName = (string)reader["ClassName"];
                        FullName = (string)reader["FullName"];
                        ApplicationDate = (DateTime)reader["ApplicationDate"];
                        PassedTestCount = Convert.ToSingle(reader["PassedTestCount"]);
                        Status = (string)reader["Status"];
                    }

                    reader.Close();
                }
                catch
                {
                    isFound = false;
                }
            }

            return isFound;
        }

        // Ajouter une nouvelle application
        public static int AddNewLocalDrivingLicenseApplications(int ApplicationID, int LicenseClassID)
        {
            int insertedID = -1;

            string query = @"INSERT INTO [dbo].[LocalDrivingLicenseApplications] ([ApplicationID], [LicenseClassID])
                             VALUES (@ApplicationID, @LicenseClassID);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            int rowsAffected = 0;

            string query = @"UPDATE [dbo].[LocalDrivingLicenseApplications]
                             SET ApplicationID = @ApplicationID,
                                 LicenseClassID = @LicenseClassID
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

            return (rowsAffected > 0);
        }

        // Obtenir toutes les applications
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM LocalDrivingLicenseApplications_View ORDER BY ClassName";

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

        // Supprimer une application par ID
        public static bool CancelLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationID)
        {
            int rowsAffected = 0;

            string query = @"UPDATE Applications
SET ApplicationStatus = 2
WHERE ApplicationID = (SELECT ApplicationID 
                       FROM LocalDrivingLicenseApplications 
                       WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID );";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch
                {
                    rowsAffected = 0;
                }
            }

            return (rowsAffected > 0);
        }


        public static bool DeleteLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationID)
        {
            int rowsAffected = 0;

            string query = @"delete LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch
                {
                    rowsAffected = 0;
                }
            }

            return (rowsAffected > 0);
        }

        // Vérifie si une application existe par ID
        public static bool IsLocalDrivingLicenseApplicationsExist(int LocalDrivingLicenseApplicationID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT 1 FROM LocalDrivingLicenseApplications_View WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

        // Vérifie si une application existe par nom et classe
        public static bool IsLocalDrivingLicenseApplicationsExistByName(string ClassName, string FullName)
        {
            bool isFound = false;

            string query = @"SELECT 1 FROM LocalDrivingLicenseApplications_View 
                             WHERE ClassName LIKE @ClassName AND FullName LIKE @FullName";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ClassName", $"%{ClassName}%");
                command.Parameters.AddWithValue("@FullName", $"%{FullName}%");

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



        // Obtenir les informations par numéro national
        public static bool GetLocalDrivingLicenseApplicationsInfoByNationalNo_ClassNameAnd_Status(
       string nationalNo, string className)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
            SELECT TOP 1 *
            FROM LocalDrivingLicenseApplications_View
            WHERE NationalNo = @NationalNo
              AND Status IN ('New', 'Completed')
              AND ClassName LIKE @className;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NationalNo", nationalNo);
                command.Parameters.AddWithValue("@className", $"%{className}%");

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    isFound = true;
            }

            return isFound;
        }


      

    }
}

         
    
