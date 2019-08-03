using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ITP213.DAL.Functions.Validations
{
    public class RegisterValidation
    {
        // Panel 1
        public static bool checkIfAdminNumberExist(string adminNo)
        {

            string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            Boolean result = false;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select * FROM student WHERE adminNo=@adminNo";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@adminNo", adminNo);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["adminNo"] != null)
                        {
                            if (reader["adminNo"] != DBNull.Value)
                            {
                                result = true; // admin number exists
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        public static bool checkIfEmailExist(string email)
        {

            string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            Boolean result = false;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select * FROM account WHERE email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["email"] != null)
                        {
                            if (reader["email"] != DBNull.Value)
                            {
                                result = true; // email exists
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
    }
}