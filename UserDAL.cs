using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ThreeTier_Arc//Similar  to the BLL, the DAL class is also renamed to UserDAL for clarity and consistency.
{
    public class UserDAL // Changed from DClass
    {
        string connString = "Server=DESKTOP-B1PDELG;Initial Catalog=UsersDB;Trusted_Connection=true";

        public DataTable GetUsers(string searchTerm = "")
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "SELECT * FROM UsersData1";
                if (!string.IsNullOrEmpty(searchTerm))
                    query += " WHERE Username LIKE @Search ORDER BY Username DESC";
                else
                    query += " ORDER BY Username DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                if (!string.IsNullOrEmpty(searchTerm))
                    cmd.Parameters.AddWithValue("@Search", "%" + searchTerm + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public int ExecuteCommand(string query, SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}