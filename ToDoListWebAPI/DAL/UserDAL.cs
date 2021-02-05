using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using ToDoListWebAPI.Models;

namespace ToDoListWebAPI.DAL
{
    public class UserDAL
    {
        private readonly IConfiguration _configuration;
        public UserDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        #region Create Logging
        public string CreateLogging(Logging log)
        {
            string result = "";
            bool isCreated = false;
            string strConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            SqlConnection myConn = new SqlConnection(strConn);
            DataTable table = new DataTable();
            try
            {
                string createQuery = @"insert into dbo.Users (Email, Username, Password, User_Type) values 
                                (@Email, @Username, @Password, @User_Type)";

                SqlCommand cmd = new SqlCommand(createQuery, myConn);
                cmd.Parameters.AddWithValue("@Email", log.Email);
                cmd.Parameters.AddWithValue("@Username", log.Username);
                cmd.Parameters.AddWithValue("@Password", log.Password);
                cmd.Parameters.AddWithValue("@User_Type", log.User_Type);

                string checkEmail = @"select count(*) from dbo.Users
                                    where Email = @Email";

                SqlCommand email_check_cmd = new SqlCommand(checkEmail,myConn);
                email_check_cmd.Parameters.AddWithValue("@Email", log.Email);
                int UserExist = (int)email_check_cmd.ExecuteScalar();

                myConn.Open();

                int rows = cmd.ExecuteNonQuery();

                isCreated = rows > 0;

                if (log.Username.Contains("RJ") && UserExist > 0 && log.Email != null)
                {
                    log.User_Type = true;
                    result = "Admin User was created successfully";
                }
                else if (UserExist > 0 && log.Email != null)
                {
                    log.User_Type = false;
                    result = "User was created sucessfully";
                }
                else
                {
                    result = "Can't create User";
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                myConn.Close();
            }
            return result;
        }
        #endregion

        #region Check Logging
        
        #endregion

        #region Delete Logging
        #endregion
    }
}
