using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDDataAccessLayer
{
    public class clsApplications
    {
        // Obtenir les informations par ID
        public static bool GetApplicationsInfoByID(
            int applicationID,
            ref int applicantPersonID,
            ref DateTime applicationDate,
            ref int applicationTypeID,
            ref int applicationStatus,
            ref DateTime lastStatusDate,
            ref float paidFees,
            ref int createdByUserID)
        {
            bool isFound = false;

            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ApplicationID", applicationID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;

                            applicantPersonID = Convert.ToInt32(reader["ApplicantPersonID"]);
                            applicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
                            applicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                            applicationStatus = Convert.ToInt32(reader["ApplicationStatus"]);
                            lastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
                            paidFees = Convert.ToSingle(reader["PaidFees"]);
                            createdByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
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
    

 
        // Ajouter une nouvelle application
        public static int AddNewApplications(
            int ApplicantPersonID,
            DateTime ApplicationDate,
            int ApplicationTypeID,
            int ApplicationStatus,
            DateTime LastStatusDate,
            float PaidFees,
            int CreatedByUserID
            )
        {
            int insertedID = -1;

            string query = @"INSERT INTO [dbo].[Applications]
           ([ApplicantPersonID]
           ,[ApplicationDate]
           ,[ApplicationTypeID]
           ,[ApplicationStatus]
           ,[LastStatusDate]
           ,[PaidFees]
           ,[CreatedByUserID])
     VALUES
           (@ApplicantPersonID,
           @ApplicationDate, 
           @ApplicationTypeID,
           @ApplicationStatus,
           @LastStatusDate,
           @PaidFees, 
           @CreatedByUserID);SELECT SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
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




        //Vérifie si une application existe par person et classe
        public static bool IsLocalDrivingLicenseApplicationsExistByName(int ApplicantPersonID, int ApplicationTypeID  )
        {
            bool isFound = false;

            string query = @"SELECT 1 FROM Applications 
                             WHERE ApplicantPersonID=@ApplicantPersonID ";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
         
                command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

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



        public static bool UpdateApplicationsStatutToCompleted(
    int ApplicationID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                int ApplicationStatus = 3;
                string query = @"UPDATE Applications
                         SET  ApplicationStatus =@ApplicationStatus 
                         WHERE ApplicationID = @ApplicationID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = ApplicationID;
                command.Parameters.Add("@ApplicationStatus", SqlDbType.Int).Value = ApplicationStatus;



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


    }
}

