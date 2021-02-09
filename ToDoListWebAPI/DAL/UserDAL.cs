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


        #region Create User - Post
        public string CreateLogging(Logging log)
        {
            string result = "";
            bool isCreated = false;
            string strConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            SqlConnection myConn = new SqlConnection(strConn);
            try
            {
                string createQuery = @"insert into dbo.Users (Email, Username, Password, User_Type) values 
                                (@Email, @Username, @Password, @User_Type)";

                if(log.Username.Contains("RJ"))
                {
                    log.User_Type = true;
                }

                SqlCommand cmd = new SqlCommand(createQuery, myConn);
                cmd.Parameters.AddWithValue("@Email", log.Email);
                cmd.Parameters.AddWithValue("@Username", log.Username);
                cmd.Parameters.AddWithValue("@Password", log.Password);
                cmd.Parameters.AddWithValue("@User_Type", log.User_Type);

                string checkEmail = @"select count(*) from dbo.Users
                                    where Email = @Email";

                SqlCommand email_check_cmd = new SqlCommand(checkEmail,myConn);
                email_check_cmd.Parameters.AddWithValue("@Email", log.Email);
                
                myConn.Open();

                int UserExist = (int)email_check_cmd.ExecuteScalar();

                int rows = cmd.ExecuteNonQuery();

                isCreated = rows > 0;

                if (UserExist == 0 && log.Email != null)
                {
                    result = "Admin User was created successfully";
                }
                else if (UserExist == 0 && log.Email != null)
                {
                    log.User_Type = false;
                    result = "Normal User was created sucessfully";
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

        #region Check User - Get
        public DataTable GetLoggingUsers()
        {
            string strConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            SqlConnection myConn = new SqlConnection(strConn);
            DataTable table = new DataTable();
            try
            {
                string usersQuery = @"select * from dbo.Users";
                SqlCommand cmd = new SqlCommand(usersQuery,myConn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                myConn.Open();
                adapter.Fill(table);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                myConn.Close();
            }
            return table;
        }
        #endregion

        #region Edit User - Put
        public string EditUser(Logging log)
        {
            string result = "Can't Update User";
            bool isEdited = false;
            string strConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            SqlConnection myConn = new SqlConnection(strConn);
            try
            {
                string editUserQuery = @" 
                                        update dbo.Users
                                        set User_Type = @User_Type
                                        where Email = @Email";
                SqlCommand cmd = new SqlCommand(editUserQuery, myConn);
                cmd.Parameters.AddWithValue("@Email", log.Email);
                cmd.Parameters.AddWithValue("@User_Type", log.User_Type);

                string checkEmail = @"select count(*) from dbo.Users
                                    where Email = @Email";
                SqlCommand email_check_cmd = new SqlCommand(checkEmail, myConn);
                email_check_cmd.Parameters.AddWithValue("@Email", log.Email);

                myConn.Open();

                int UserExist = (int)email_check_cmd.ExecuteScalar();

                int rows = cmd.ExecuteNonQuery();

                // if(rows>0) {isEdit == true}
                isEdited = rows > 0;

                if(UserExist == 0)
                {
                    result = "Your Email doens't exist!! Check it!";
                }
                else
                {
                    result = "Updated Successfully";
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

        #region Delete Users - Delete
        public string DeleteUser(int id)
        {
            string result = "";
            bool isDeleted = false;
            string strConn = _configuration["ConnectionStrings:ToDoListAppCon"];
            SqlConnection myConn = new SqlConnection(strConn);
            try
            {
                string deleteUSerquery = @"
                    delete from dbo.Users 
                    where id= @id";
                SqlCommand cmd = new SqlCommand(deleteUSerquery, myConn);
                cmd.Parameters.AddWithValue("@id", id);
                myConn.Open();

                //ExecuteNonQuery valida quantas rows foram afectadas
                int rows = cmd.ExecuteNonQuery();

                isDeleted = rows > 0;
                result = (isDeleted == true) ? "Deleted Successfully" : "Can't Delete Users";
            }
            catch (Exception ex)
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


    }
}
