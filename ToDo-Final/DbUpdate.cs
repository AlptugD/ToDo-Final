using System;
using Microsoft.Data.SqlClient;

internal static class DbUpdate
{
    public static void Run  ()
    {
        string connectionString = @"Server=AD\SQLEXPRESS;Database=ASyncTaskDB;Trusted_Connection=True;TrustServerCertificate=True;";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            try
            {
                using (SqlCommand cmd = new SqlCommand("EXEC sp_rename 'Tasks.TaskDate', 'StartDate', 'COLUMN';", conn))
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Renamed TaskDate to StartDate");
                }
            }
            catch (Exception ex) { Console.WriteLine("Rename failed or already done: " + ex.Message); }

            try
            {
                using (SqlCommand cmd = new SqlCommand("ALTER TABLE Tasks ADD EndDate DATETIME;", conn))
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Added EndDate");
                }
            }
            catch (Exception ex) { Console.WriteLine("Add EndDate failed or already exists: " + ex.Message); }
        }
    }
}
